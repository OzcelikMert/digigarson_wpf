using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Digigarson.Classes.FileControl
{
    class Read
    {
        /// <summary>
        /// Read File
        /// </summary>
        /// <returns>Readed File Texts</returns>
        public string[] _Read() {
            List<string> values = new List<string>();
            try {
                Common.checkFile(Values.FolderLocation, Values.FileName);
                // Read
                using (StreamReader file = new StreamReader(Values.FolderLocation + Values.FileName)) {
                    while (file.ReadLine() != null) {
                        values.Add(file.ReadLine());
                    }
                    file.Close();
                }
            } catch (Exception) {
                MessageBox.Show("Günlük log okunamadı.", "Günlük Log", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            return values.ToArray();
        }
    }
}
