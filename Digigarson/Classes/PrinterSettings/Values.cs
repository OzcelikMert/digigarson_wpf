using System;
using System.Data.SQLite;

namespace Digigarson.Classes.PrinterSettings
{
    class Values
    {
        public string DBName = "PrinterSettings.db";
        public string FolderLocation = AppDomain.CurrentDomain.BaseDirectory + "/Database/";
        public string DBVersion = "1.0";
        public SQLiteConnection ConnectDB { get; set; }
    }
}
