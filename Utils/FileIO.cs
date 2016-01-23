using System;
using System.IO;

using UnityEngine;

namespace Ent2D.Utils {
    public static class FileIO {
        public static string LoadStringFromFile(string key) {
            string result = "";

            if (!LoadStringFromPersistentDataPath(key, ref result)) {
                if (!LoadStringFromResources(key, ref result)) {
                    Debug.LogErrorFormat("[FileIO] Could not load {0} from Resources, please create a default file at {1}",
                            key, GetResourcePathFromKey(key));
                } else {
                    SaveStringToFile(result, key);
                }
            }
            return result;
        }

        public static void SaveStringToFile(string data, string key) {
            SaveStringToDataPath(data, key);
        }

        private static bool LoadStringFromPersistentDataPath(string key, ref string result) {
            string path = GetPersistentPathFromKey(key);
            FileInfo fileInfo = new FileInfo(path);
            if (!fileInfo.Exists) {
                result = string.Format("[FileIO] File Not Exist error: {0}", path);
                Debug.LogFormat("{0}", result);
                return false;
            }

            try {
                using (FileStream fileStream = fileInfo.OpenRead()) {
                    using (StreamReader reader = new StreamReader(fileStream)) {
                        result = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex) {
                result = string.Format("Exception reading file at {0}", path);
                Debug.LogFormat("{0}", result);
                return false;
            }

            return true;
        }

        private static void SaveStringToDataPath(string data, string key) {
            string path = GetPersistentPathFromKey(key);

            Directory.CreateDirectory(Path.GetDirectoryName(path));
            try {
                using (StreamWriter writer = new StreamWriter(path, false)) {
                    writer.Write(data);
                }
                Debug.LogFormat("saved to {0}", path);
            } catch (Exception ex) {
                Debug.LogFormat("Exception writing file at {0}: {1}", path, ex.Message);
            }
        }

        private static bool LoadStringFromResources(string key, ref string result) {
            string path = GetResourcePathFromKey(key);
            Debug.LogFormat("resource path: {0}", path);
            TextAsset text = Resources.Load(path) as TextAsset;

            if (text == null) {
                Debug.LogErrorFormat("Failed to load file from path: {0}", path);
                return false;
            }

            result = text.text;

            return true;
        }

        private static string GetPersistentPathFromKey(string key) {
            return string.Format("{0}/Config/{1}/config.json",
                    Application.dataPath, key);
        }

        private static string GetResourcePathFromKey(string key) {
            return string.Format("Config/{0}/config", key);
        }
    }
}

