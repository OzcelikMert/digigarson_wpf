using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace Digigarson_Loader.Updater
{
    /// <summary>
    /// The interface that all applications need to implement in order to user CSharpUpdater
    /// </summary>
    class UpdaterValues
    {
        /// <summary>
        /// The path of your application
        /// </summary>
        public string ApplicationPath { get; }

        /// <summary>
        /// The name of your application as you want it displayed on the update form
        /// </summary>
        public string ApplicationName { get; }

        /// <summary>
        /// The current assembly
        /// </summary>
        public Assembly ApplicationAssembly { get; }

        /// <summary>
        /// The application's icon to be displayed in the top left
        /// </summary>
        public ImageSource ApplicationIcon { get; }

        /// <summary>
        /// The context of the program.
        /// For Windows Forms Applications, use 'this'
        /// Console Apps, reference System.Windows.Forms and return null.
        /// </summary>
        public Window Context { get; }

        /// <summary>
        /// The version of your application
        /// </summary>
        public Version Version { get; }

        /// <summary>
        /// Tag to distinguish types of updates
        /// </summary>
        public UpdaterTagType Tag;

        public UpdaterValues(UpdaterXml job, Assembly assembly, Window window) {
            ApplicationPath = job.FilePath;
            ApplicationName = Path.GetFileNameWithoutExtension(ApplicationPath);
            ApplicationAssembly = assembly;
            ApplicationIcon = window.Icon;
            Context = window;
            Version = (job.Tag == UpdaterTagType.UPDATE) ? ApplicationAssembly.GetName().Version : job.Version;
            Tag = job.Tag;
        }

        public UpdaterValues(UpdaterXml job) {
            ApplicationPath = job.FilePath;
            ApplicationName = Path.GetFileNameWithoutExtension(ApplicationPath);
            ApplicationAssembly = (job.Tag == UpdaterTagType.UPDATE) ? Assembly.Load(ApplicationName) : null;
            ApplicationIcon = null;
            Context = null;
            Version = (job.Tag == UpdaterTagType.UPDATE) ? ApplicationAssembly.GetName().Version : job.Version;
            Tag = job.Tag;
        }

        public void Print() {
            string head = "========== CSharpUpdaterValues ==========";
            string tail = "=============================================";
            string toPrint = string.Format("{0}\nTag Type: {1}\nApplicationPath: {2}\nApplicationName: {3}\nAssemblyName: {4}\nFormName: {5}\nVersion: {6}\n{7}",
                head, Tag.ToString(), ApplicationPath == null ? "null" : ApplicationPath,
                ApplicationName == null ? "null" : ApplicationName,
                ApplicationAssembly == null ? "null" : ApplicationAssembly.FullName,
                Context == null ? "null" : Context.Name,
                Version == null ? "null" : Version.ToString(), tail);
            Console.WriteLine(toPrint);
        }
    }
}
