using System;
using System.Windows;

namespace Digigarson.Classes.JSInterfaces
{
    public class WindowStatus
    {
        private Window window { get; set; }
        private FileControl.Write write { get; set; }

        public WindowStatus(Window _window) {
            this.window = _window;
        }

        // Status: Exit
        public void _Exit() {
            try {
                window.Dispatcher.Invoke(() => {
                    window.Hide();
                    Application.Current.Shutdown();
                });
            } catch (Exception exception) {
                write = new FileControl.Write(DateTime.Now.ToString("dd/MM/yyyy H:mm") + " : Error : " + exception.ToString());
                write._Write();
            }
        }

        // Status: Hide
        public void _Hide() {
            try {
                window.Dispatcher.Invoke(() => {
                    window.WindowState = WindowState.Minimized;
                });
            } catch (Exception exception) {
                write = new FileControl.Write(DateTime.Now.ToString("dd/MM/yyyy H:mm") + " : Error : " + exception.ToString());
                write._Write();
            }
        }

        // Status: Window Mode
        public void _WindowMode() {
            try {
                window.Dispatcher.Invoke(() => {
                    if (window.WindowStyle == WindowStyle.None)
                        window.WindowStyle = WindowStyle.SingleBorderWindow;
                    else
                        window.WindowStyle = WindowStyle.None;
                    window.WindowState = WindowState.Maximized;
                });
            } catch (Exception exception) {
                write = new FileControl.Write(DateTime.Now.ToString("dd/MM/yyyy H:mm") + " : Error : " + exception.ToString());
                write._Write();
            }
        }
    }
}
