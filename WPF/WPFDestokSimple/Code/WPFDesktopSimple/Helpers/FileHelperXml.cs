using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace WPFDesktopSimple.Helpers
{
    public class FileHelperXml : IFileHelper
    {
        public void SerializeToFile<T>(string filePath, T dataToSerialize) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (FileStream file = File.Create(filePath))
                {
                    serializer.Serialize(file, dataToSerialize);
                }
            }
            catch (Exception e)
            {
                // an exception occured file non conform;
            }
        }

        public bool DeserializeFromFile<T>(string filePath, out T data, string rootAttribute) where T : class
        {
            Type typeParameterType = typeof(T);
            XmlSerializer serializer = new XmlSerializer(typeParameterType, new XmlRootAttribute(rootAttribute));
            try
            {
                using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8, true))
                {
                    data = serializer.Deserialize(reader) as T;
                }
            }
            catch (Exception e)
            {
                // an exception occured file non conform;
                data = null;
            }

            return data != null;
        }
    }
}
