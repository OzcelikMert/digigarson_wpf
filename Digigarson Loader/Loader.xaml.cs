using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Digigarson_Loader.Updater;

namespace Digigarson_Loader
{
    /// <summary>
    /// Interaction logic for Loader.xaml
    /// </summary>
    public partial class Loader : Window
    {
        public Loader() {
            InitializeComponent();
            // Check is Running
            if (Process.GetProcessesByName("Digigarson Loader").Length > 1) {
                // If ther is more than one, than it is already running.
                System.Windows.Application.Current.Shutdown();
            }
        }

        public bool IsProcessOpen(string name) {
            foreach (Process clsProcess in Process.GetProcesses()) {
                if (clsProcess.ProcessName.ToLower().Contains(name.ToLower())) {
                    return true;
                }
            }
            return false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            versionName.Content = "v" + Application.ResourceAssembly.GetName().Version.ToString();
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                Dispatcher.Invoke(new Action(() => {
                    UpdaterInitialize updaterInitialize = new UpdaterInitialize(Assembly.GetExecutingAssembly(), this, new Uri("https://digigarson.com/dg/management/updated_files/xml/update_info.xml"), false);
                    updaterInitialize.DoUpdate();
                }));
            });
        }
    }
}
