namespace WPFDesktopSimple.Helpers
{
    public interface IFileHelper
    {
        void SerializeToFile<T>(string filePath, T dataToSerialize) where T : class;
        bool DeserializeFromFile<T>(string filePath, out T data, string rootAttribute) where T : class;
    }
}
