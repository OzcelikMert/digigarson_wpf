using CefSharp;
using System.Windows;

namespace Digigarson.Classes.Browser
{
    class BrowserFunctions
    {
        private Window window { get; set; }

        public BrowserFunctions(Window _window) {
            this.window = _window;
        }

        public void AddressChanged(object sender, AddressChangedEventArgs args) {
            //MessageBox.Show("Changed Page: " + args.Address);
        }

        public void LoadingStateChanged(object sender, LoadingStateChangedEventArgs args) {
            /*if (!args.IsLoading) {
                MessageBox.Show("Page Loaded");
            }*/
        }

    }
}
