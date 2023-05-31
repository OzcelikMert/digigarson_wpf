using System;
using CefSharp;
using CefSharp.WinForms;
using System.Windows.Forms;
using System.Windows;

namespace Digigarson.Classes.Browser
{
    class InitializeBrowser
    {
        private Window window { get; set; }
        public InitializeBrowser(Window _window) {
            this.window = _window;
        }

        /* Browser Functions */
        public ChromiumWebBrowser Initialize(ChromiumWebBrowser chromiumWebBrowser, System.Windows.Forms.Control control) {
            CefSettings cefSettings = new CefSettings();
            cefSettings.CefCommandLineArgs.Add("enable-gpu", "1");
            cefSettings.CefCommandLineArgs["touch-events"] = "enabled";
            cefSettings.RemoteDebuggingPort = 8088;
            // Initialize Cef With the provided settings
            Cef.Initialize(cefSettings);
            // Create a browser component
            chromiumWebBrowser = new ChromiumWebBrowser(Values.HomeURL);
            // Add the browser on the Window
            control.Controls.Add(chromiumWebBrowser);
            // Make the browser fill the form
            chromiumWebBrowser.Dock = DockStyle.Fill;
            // (**WPF Bug!**) When we use Windows Form in WPF, Touch Screen is closes. This code open touchscreen events.
            AppContext.SetSwitch("Switch.System.Windows.Input.Stylus.DisableStylusAndTouchSupport", true);
            // Set browser handlers
            chromiumWebBrowser.MenuHandler = new CustomMenuHandler();
            chromiumWebBrowser.DownloadHandler = new DownloadHandler();
            // Browser JS Interface Enabled
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            CefSharpSettings.WcfEnabled = true;
            // Set Browser Functions
            BrowserFunctions browserFunctions = new BrowserFunctions(this.window);
            chromiumWebBrowser.AddressChanged += browserFunctions.AddressChanged;
            chromiumWebBrowser.LoadingStateChanged += browserFunctions.LoadingStateChanged;
            // Set Javascript Interface Classes
            chromiumWebBrowser.JavascriptObjectRepository.Register("Application_WindowStatus", new Classes.JSInterfaces.WindowStatus(window), isAsync: false, options: BindingOptions.DefaultBinder);
            chromiumWebBrowser.JavascriptObjectRepository.Register("Application_PrinterSettings", new Classes.JSInterfaces.PrinterSettings(window), isAsync: false, options: BindingOptions.DefaultBinder);
            chromiumWebBrowser.JavascriptObjectRepository.Register("Application_Others", new Classes.JSInterfaces.Others(window), isAsync: false, options: BindingOptions.DefaultBinder);
            chromiumWebBrowser.JavascriptObjectRepository.Register("Application_Values", new Classes.JSInterfaces.Values(), isAsync: false, options: BindingOptions.DefaultBinder);
            // Return Browser Saved Values
            return chromiumWebBrowser;
        }
        /* end Browser Functions */
    }
}
