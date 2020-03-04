namespace WPFDesktopSimple.Helpers
{
    public interface IInteractionHelper
    {
        void ProvideFeedback(string msg);
        bool AskQuestion(string msg, params object[] args);

        bool OpenFile(out string filePath, params object[] args);
        bool SaveFile(out string filePath, params object[] args);
    }
}
