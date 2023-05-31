using System;
using System.IO;
using System.Windows;

namespace Digigarson.Classes.FileControl
{
    class Write
    {
        private string Text { get; set; }
        private string[] ArrayText { get; set; }

        /// <summary>
        /// Write string type text to file
        /// </summary>
        /// <param name="fileLocation">File Location</param>
        /// <param name="text">String Type Text</param>
        public Write(string text) => this.Text = text;

        /// <summary>
        /// Write array string type text to file
        /// </summary>
        /// <param name="fileLocation">File Location</param>
        /// <param name="text">Array String Type Text</param>
        public Write(string[] text) => this.ArrayText = text;
        /// <summary>
        /// Write Text To File
        /// </summary>
        public void _Write() {
            try {
                Common.checkFile(Values.FolderLocation, Values.FileName);
                // Write
                using (StreamWriter file = File.AppendText(Values.FolderLocation + Values.FileName)) {
                    // Array String
                    if (ArrayText != null && ArrayText.Length > 0) {
                        foreach (string text in this.ArrayText) {
                            file.WriteLine(text);
                        }
                    }
                    // String
                    file.WriteLine(this.Text);
                    file.Close();
                }
            } catch (System.Exception exception) {
                MessageBox.Show("Günlük log yazıdırlamadı.\nHata: " + exception.ToString(), "Günlük Log", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
