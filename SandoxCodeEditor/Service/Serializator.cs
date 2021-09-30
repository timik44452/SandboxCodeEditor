using System.IO;
using System.Xml.Serialization;

namespace SandboxCodeEditor.Sevice
{
    public static class Serializator
    {
        public static void Serialization(string path, object serializationObject)
        {
            if (serializationObject == null)
            {
                return;
            }

            var serializer = new XmlSerializerFactory().CreateSerializer(serializationObject.GetType());

            using (Stream stream = new StreamWriter(path).BaseStream)
            {
                serializer.Serialize(stream, serializationObject);
            }
        }

        public static T Deserialization<T>(string path)
        {
            if (File.Exists(path))
            {
                var serializer = new XmlSerializerFactory().CreateSerializer(typeof(T));

                using (Stream stream = new StreamReader(path).BaseStream)
                {
                    object deserializedObject = serializer.Deserialize(stream);

                    if (deserializedObject is T)
                        return (T)deserializedObject;
                }
            }
            return default;
        }
    }
}