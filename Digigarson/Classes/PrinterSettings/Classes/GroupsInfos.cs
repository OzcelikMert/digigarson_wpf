using System.Collections.Generic;

namespace Digigarson.Classes.PrinterSettings.Classes
{
    public class GroupsInfos
    {
        public string GroupID { get; set; }
        public string GroupName { get; set; }
        public string PrinterName { get; set; }
        public List<GroupsInfosProductCategory> ProductCategories { get; set; }
    }
}
