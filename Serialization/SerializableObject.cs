using System;
using System.Collections.Generic;

using UnityEngine;

namespace Ent2D.Serialization {
    public abstract class SerializableObject {
        private JSONObject _SerializableObjectJson = null;
        public JSONObject SerializableObjectJson {
            get { return _SerializableObjectJson; }
            set {
                if (_SerializableObjectJson == null) {
                    _SerializableObjectJson = value;
                } else {
                    Debug.LogErrorFormat(
                            "Cannot set initialized SerializableObjectJson. Use Reserialize instead.");
                }
            }
        }

        public abstract void LoadSerializableObject();
        protected abstract void Serialize();

        public void Reserialize() {
            _SerializableObjectJson = new JSONObject();
            Serialize();
        }

        public override string ToString() {
            return SerializableObjectJson.ToString(true);
        }

        protected bool Load<T>(string key, ref T val) {
            if (_SerializableObjectJson == null) {
                Debug.LogErrorFormat( "[CONFIG] Can't get {0} as serializableObject not loaded", key);
                return false;
            }

            T convertedVal = val;
            _SerializableObjectJson.GetField(key,
                    delegate(JSONObject o) {
                        Type type = typeof(T);
                        if (type == typeof(int)) {
                            convertedVal = (T) Convert.ChangeType((int) Mathf.Floor(o.n), type);
                        } else if (type == typeof(float)) {
                            convertedVal = (T) Convert.ChangeType(o.n, type);
                        } else if (type == typeof(string)) {
                            convertedVal = (T) Convert.ChangeType(o.str, type);
                        } else if (type == typeof(bool)) {
                            convertedVal = (T) Convert.ChangeType(o.b, type);
                        }
                    },
                    LoadError);
            val = convertedVal;
            return true;
        }

        protected bool LoadSubSerializableObjects<T>(string key, ref List<T> val) where T : SubSerializableObject, new() {
            if (!CheckJson()) return false;

            List<T> subSerializableObjects = new List<T>();
            string subSerializableObjectName = GetSubSerializableObjectName(typeof(T));

            _SerializableObjectJson.GetField(key,
                    delegate(JSONObject o) {
                        if (o == null || o.list == null) return;

                        foreach (JSONObject jObj in o.list) {
                            T subSerializableObject = new T();

                            //TODO: Possible extension- do something for checking errors when loading
                            subSerializableObject.LoadSerializableObject(jObj);
                            subSerializableObjects.Add(subSerializableObject);
                        }
                    },
                    LoadError);
            val = subSerializableObjects;
            return true;
        }

        protected void Save<T>(string key, T val) {
            Type type = typeof(T);
            if (type == typeof(int)) {
                int convertedVal = (int) Convert.ChangeType(val, typeof(int));
                SaveInt(key, convertedVal);
            } else if (type == typeof(float)) {
                float convertedVal = (float) Convert.ChangeType(val, typeof(float));
                SaveFloat(key, convertedVal);
            } else if (type == typeof(string)) {
                string convertedVal = (string) Convert.ChangeType(val, typeof(string));
                SaveString(key, convertedVal);
            } else if (type == typeof(bool)) {
                bool convertedVal = (bool) Convert.ChangeType(val, typeof(bool));
                SaveBool(key, convertedVal);
            }
        }

        protected void Save<T>(string key, List<T> val) where T : SubSerializableObject {
            _SerializableObjectJson.AddField(key,
                    delegate(JSONObject jObj)
                    {
                        foreach (T subSerializableObject in val)
                        {
                            subSerializableObject.Reserialize();
                            jObj.Add(subSerializableObject.SerializableObjectJson);
                        }
                    });
        }

        private void SaveInt(string key, int val) {
            _SerializableObjectJson.AddField(key, val);
        }
        private void SaveFloat(string key, float val) {
            _SerializableObjectJson.AddField(key, val);
        }
        private void SaveString(string key, string val) {
            _SerializableObjectJson.AddField(key, val);
        }
        private void SaveBool(string key, bool val) {
            _SerializableObjectJson.AddField(key, val);
        }

        private void LoadError(string key) {
            Debug.LogFormat(
                "[CONFIG] <color=teal>Couldn't find {0} in serializableObject {1}, using default</color>",
                key, _SerializableObjectJson);
        }

        private string GetFolderName(Type type) {
            return string.Format("{0}s", GetSubSerializableObjectName(type));
        }

        private string GetSubSerializableObjectName(Type type) {
            string tStr = type.ToString();
            string[] decomposedString = tStr.Split('.');
            if (decomposedString.Length <= 0)
            {
                return "";
            }
            else
            {
                return decomposedString[decomposedString.Length-1];
            }
        }

        private bool CheckJson() {
            if (_SerializableObjectJson == null) {
                Debug.LogErrorFormat( "[CONFIG] SerializableObject not loaded");
                return false;
            }
            return true;
        }
    }
}

