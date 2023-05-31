using System.Collections.Generic;

namespace Digigarson.Classes.PrinterSettings.Classes
{
    public class PrinterOptions
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public List<Contents> Contents { get; set; }
    }
}
