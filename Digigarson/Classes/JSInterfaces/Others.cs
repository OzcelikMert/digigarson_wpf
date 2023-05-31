using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Digigarson.Classes.JSInterfaces
{
    class Others
    {
        private Window window { get; set; }
        public Others(Window _window) {
            this.window = _window;
        }
        private FileControl.Write write { get; set; }
        private FileControl.Read read { get; set; }

        public void writeLog(string LogText) {
            write = new FileControl.Write(DateTime.Now.ToString("dd/MM/yyyy H:mm") + " : Browser Log : " + LogText);
            write._Write();
        }

        public string[] readLog() {
            read = new FileControl.Read();
            return read._Read();
        }
    }
}
