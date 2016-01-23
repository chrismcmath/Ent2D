namespace Ent2D.Serialization {
    public abstract class SubSerializableObject : SerializableObject {
        public void LoadSerializableObject(JSONObject serializableObjectJson) {
            SerializableObjectJson = serializableObjectJson;
            LoadSerializableObject();
        }
    }
}
