using System.Collections.Generic;

using UnityEngine;

using Ent2D.Utils;

namespace Ent2D.Serialization {
    //TODO: call this something like 'root'?
    public abstract class BaseSerializableObject : SerializableObject {
        public BaseSerializableObject() : base() {
            SerializableObjectJson = LoadJObjectFromFile(GetSerializableObjectKey());
            LoadSerializableObject();
        }

        public void OnSaveSerializableObject() {
            Reserialize();
            FileIO.SaveStringToFile(this.ToString(), GetSerializableObjectKey());
        }

        protected JSONObject LoadJObjectFromFile(string path) {
            string jsonStr = FileIO.LoadStringFromFile(path);
            return new JSONObject(jsonStr);
        }

        protected abstract string GetSerializableObjectKey();
    }
}

