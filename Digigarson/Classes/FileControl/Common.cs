using System.IO;

namespace Digigarson.Classes.FileControl
{
    class Common
    {
        public static void checkFile(string folderLocation, string fileName) {
            if (!Directory.Exists(folderLocation)) {
                Directory.CreateDirectory(folderLocation);
            }

            if (!File.Exists(folderLocation + fileName)) {
                File.Create(folderLocation + fileName).Close();
            }
        }
    }
}
