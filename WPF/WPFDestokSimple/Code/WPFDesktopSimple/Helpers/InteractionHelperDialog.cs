using Microsoft.Win32;
using System;
using System.Windows;

namespace WPFDesktopSimple.Helpers
{
    public class InteractionHelperDialog : IInteractionHelper
    {
        public bool AskQuestion(string msg, params object[] args)
        {
            var result = MessageBox.Show(msg, "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            return result == MessageBoxResult.Yes;
        }

        public bool OpenFile(out string filePath, params object[] args)
        {
            if (args.Length < 2)
            {
                throw new ArgumentException($"'{nameof(OpenFileDialog.Multiselect)}' and '{nameof(OpenFileDialog.Filter)}' are mandatory arguments");
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = (bool)args[0];
            openFileDialog.Filter = (string) args[1];
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var result = openFileDialog.ShowDialog() == true;
            filePath = result ? openFileDialog.FileName : string.Empty;

            return result;
        }

        public void ProvideFeedback(string msg)
        {
            MessageBox.Show(msg, "Confirmation");
        }

        public bool SaveFile(out string filePath, params object[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException($"'{nameof(OpenFileDialog.Filter)}' is a mandatory arguments");
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Filter = args[0] as string;
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var result = saveFileDialog.ShowDialog() == true;
            filePath = result ? saveFileDialog.FileName : string.Empty;

            return result;
        }
    }
}
