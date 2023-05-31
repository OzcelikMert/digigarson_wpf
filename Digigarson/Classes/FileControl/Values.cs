using System;

namespace Digigarson.Classes.FileControl
{
    class Values
    {
        public static string FileName {
            get { return "LOG_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt"; }
        }

        public static string FolderLocation {
            get { return AppDomain.CurrentDomain.BaseDirectory + "/Log/"; }
        }
    }
}
