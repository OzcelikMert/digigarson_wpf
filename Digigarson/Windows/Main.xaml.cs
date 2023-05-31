using System;
using System.Windows;
using Digigarson.Classes.Browser;
using CefSharp;
using CefSharp.WinForms;
using System.Diagnostics;

namespace Digigarson.Windows
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        // Element Variables
        private ChromiumWebBrowser Digigarson_Browser;
        // Log Write
        private Classes.FileControl.Write write { get; set; }

        public Main() {
            InitializeComponent();
            // Check is Running
            if (Process.GetProcessesByName("Digigarson").Length > 1) {
                // If ther is more than one, than it is already running.
                System.Windows.Application.Current.Shutdown();
            }
            // Start Browser
            PrinterDB();
            startBrowser();
        }

        /* Browser Functions */
        private void startBrowser() {
            try {
                // Initialize Browser
                InitializeBrowser initializeBrowser = new InitializeBrowser(this);
                Digigarson_Browser = initializeBrowser.Initialize(Digigarson_Browser, this.MainWFHControl);
            } catch (Exception exception) {
                write = new Classes.FileControl.Write(DateTime.Now.ToString("dd/MM/yyyy H:mm") + " : Error : " + exception.ToString());
                write._Write();
                MessageBox.Show("Browser başlatılamadı.", "Başlatma Mesajı", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        /* end Browser Functions */

        /* Window Functions */
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            Cef.Shutdown();
        }
        /* end Window Functions */

        /* DB Functions */
        private void PrinterDB() {
            try {
                Classes.PrinterSettings.Values DBvalues = new Classes.PrinterSettings.Values();
                Classes.PrinterSettings.CreateDB createDB = new Classes.PrinterSettings.CreateDB(DBvalues.DBVersion);
                createDB.Create();
            } catch (Exception exception) {
                write = new Classes.FileControl.Write(DateTime.Now.ToString("dd/MM/yyyy H:mm") + " : Error : " + exception.ToString());
                write._Write();
                MessageBox.Show("Yazıcı ayarları oluşturulamadı.", "Başlatma Mesajı", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        /* end DB Functions */
    }
}
