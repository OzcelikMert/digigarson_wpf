using System.Collections.Generic;
using System.Data.SQLite;
using Digigarson.Classes.PrinterSettings.Classes;

namespace Digigarson.Classes.PrinterSettings
{
    class SelectFunctions
    {
        Values DBValues = new Values();
        // Get Groups
        public List<Groups> getGroups() {
            List<Groups> values = new List<Groups>();
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                using (SQLiteCommand GetGroups = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    GetGroups.CommandText = "select * from groups";
                    using (SQLiteDataReader GroupsReader = GetGroups.ExecuteReader()) {
                        while (GroupsReader.Read()) {
                            values.Add(new Groups {
                                GroupID = GroupsReader["group_id"].ToString(),
                                GroupName = GroupsReader["group_name"].ToString(),
                                PrinterName = GroupsReader["printer_name"].ToString()
                            });
                        }
                    }
                }
                DBValues.ConnectDB.Close();
            }
            return values;
        }
        // Get Group Product Categories
        public List<GroupProductCategories> getGroupProductCategories(string GroupID) {
            List<GroupProductCategories> values = new List<GroupProductCategories>();
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                using (SQLiteCommand GetGroupProductCategories = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    GetGroupProductCategories.CommandText = "select * from group_product_categories where group_id=@group_id";
                    GetGroupProductCategories.Parameters.AddWithValue("@group_id", GroupID);
                    using (SQLiteDataReader GetGroupProductCategoriesReader = GetGroupProductCategories.ExecuteReader()) {
                        while (GetGroupProductCategoriesReader.Read()) {
                            values.Add(new GroupProductCategories {
                                GroupID = GetGroupProductCategoriesReader["group_id"].ToString(),
                                ProductCategoryID = GetGroupProductCategoriesReader["product_category_id"].ToString()
                            });
                        }
                    }
                }
                DBValues.ConnectDB.Close();
            }
            return values;
        }
        // Get Group Category Products
        public List<string> getGroupCategoryProducts(string GroupID, string ProductCategoryID) {
            List<string> values = new List<string>();
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                using (SQLiteCommand GetGroupCategoryProducts = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    GetGroupCategoryProducts.CommandText = "select * from group_removed_products where group_id=@group_id and product_category_id=@product_category_id";
                    GetGroupCategoryProducts.Parameters.AddWithValue("@group_id", GroupID);
                    GetGroupCategoryProducts.Parameters.AddWithValue("@product_category_id", ProductCategoryID);
                    using (SQLiteDataReader GetGroupCategoryProductsReader = GetGroupCategoryProducts.ExecuteReader()) {
                        while (GetGroupCategoryProductsReader.Read()) {
                            values.Add(GetGroupCategoryProductsReader["product_id"].ToString());
                        }
                    }
                }
                DBValues.ConnectDB.Close();
            }
            return values;
        }
        public string[] getGroupCategoryProducts(string GroupID) {
            List<string> values = new List<string>();
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                using (SQLiteCommand GetGroupCategoryProducts = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    GetGroupCategoryProducts.CommandText = "select * from group_removed_products where group_id=@group_id";
                    GetGroupCategoryProducts.Parameters.AddWithValue("@group_id", GroupID);
                    using (SQLiteDataReader GetGroupCategoryProductsReader = GetGroupCategoryProducts.ExecuteReader()) {
                        while (GetGroupCategoryProductsReader.Read()) {
                            values.Add(GetGroupCategoryProductsReader["product_id"].ToString());
                        }
                    }
                }
                DBValues.ConnectDB.Close();
            }
            return values.ToArray();
        }
        // Check Group
        public bool checkGroup(string GroupID) {
            bool value = false;
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                using (SQLiteCommand CheckGroup = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    CheckGroup.CommandText = "select * from groups where group_id=@group_id";
                    CheckGroup.Parameters.AddWithValue("@group_id", GroupID);
                    using (SQLiteDataReader CheckGroupReader = CheckGroup.ExecuteReader()) {
                        if (CheckGroupReader.Read()) {
                            value = true;
                        }
                    }
                }
                DBValues.ConnectDB.Close();
            }
            return value;
        }
        // Get Printer Options
        public List<PrinterOptions> getPrinterOptions() {
            List<PrinterOptions> values = new List<PrinterOptions>();
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                using (SQLiteCommand GetPrinterOptions = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    GetPrinterOptions.CommandText = "select * from options";
                    using (SQLiteDataReader GetPrinterOptionsReader = GetPrinterOptions.ExecuteReader()) {
                        while (GetPrinterOptionsReader.Read()) {
                            List<Contents> contents = getPrinterOptionContents(GetPrinterOptionsReader["option_name"].ToString());
                            values.Add(new PrinterOptions {
                                Name = GetPrinterOptionsReader["option_name"].ToString(),
                                Value = GetPrinterOptionsReader["value"].ToString(),
                                Contents = contents
                            });
                        }
                    }
                }
            }
            return values;
        }
        // Get Printer Option Contents
        public List<Contents> getPrinterOptionContents(string OptionName) {
            List<Contents> values = new List<Contents>();
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                using (SQLiteCommand GetPrinterOptionsContent = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    GetPrinterOptionsContent.CommandText = "select * from options_content where option_name=@option_name";
                    GetPrinterOptionsContent.Parameters.AddWithValue("@option_name", OptionName);
                    using (SQLiteDataReader GetPrinterOptionsContentReader = GetPrinterOptionsContent.ExecuteReader()) {
                        while (GetPrinterOptionsContentReader.Read()) {
                            values.Add(new Contents {
                                Name = GetPrinterOptionsContentReader["option_content_name"].ToString(),
                                Value = GetPrinterOptionsContentReader["value"].ToString()
                            });
                        }
                    }
                }
                DBValues.ConnectDB.Close();
            }
            return values;
        }
        // Get Groups Info
        public List<GroupsInfos> getGroupsInfos() {
            List<GroupsInfos> values = new List<GroupsInfos>();
            List<GroupsInfosProductCategory> ProductCategories = new List<GroupsInfosProductCategory>();
            List<string> Products = new List<string>();
            string GroupID = "";
            string GroupName = "";
            string PrinterName = "";
            string ProductCategoryID = "";
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                using (SQLiteCommand GetGroupsInfos = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    GetGroupsInfos.CommandText = @"
                        select
                        groups.group_id,
                        groups.group_name,
                        groups.printer_name,
                        group_product_categories.product_category_id,
                        group_removed_products.product_id
                        from groups
                        left outer join group_product_categories on group_product_categories.group_id = groups.group_id
                        left outer join group_removed_products on group_removed_products.group_id = group_product_categories.group_id
                        group by groups.group_id, group_product_categories.product_category_id, group_removed_products.product_id
                        order by groups.group_id, group_product_categories.product_category_id asc";
                    using (SQLiteDataReader GetGroupsInfosReader = GetGroupsInfos.ExecuteReader()) {
                        while (GetGroupsInfosReader.Read()) {
                            if (!string.IsNullOrEmpty(GroupID)) {
                                // Check New Product Category ID
                                if (GroupID != GetGroupsInfosReader["group_id"].ToString() || ProductCategoryID != GetGroupsInfosReader["product_category_id"].ToString()) {
                                    ProductCategories.Add(new GroupsInfosProductCategory {
                                        ProductCategoryID = ProductCategoryID,
                                        Products = Products
                                    });
                                    Products = new List<string>();
                                }
                                // Check New Group
                                if (GroupID != GetGroupsInfosReader["group_id"].ToString()) {
                                    values.Add(new GroupsInfos {
                                        GroupID = GroupID,
                                        GroupName = GroupName,
                                        PrinterName = PrinterName,
                                        ProductCategories = ProductCategories
                                    });
                                    ProductCategories = new List<GroupsInfosProductCategory>();
                                    Products = new List<string>();
                                }
                            }
                            // Set Values
                            GroupID = GetGroupsInfosReader["group_id"].ToString();
                            GroupName = GetGroupsInfosReader["group_name"].ToString();
                            PrinterName = GetGroupsInfosReader["printer_name"].ToString();
                            ProductCategoryID = GetGroupsInfosReader["product_category_id"].ToString();
                            Products.Add(GetGroupsInfosReader["product_id"].ToString());
                        }
                        ProductCategories.Add(new GroupsInfosProductCategory {
                            ProductCategoryID = ProductCategoryID,
                            Products = Products
                        });
                        values.Add(new GroupsInfos {
                            GroupID = GroupID,
                            GroupName = GroupName,
                            PrinterName = PrinterName,
                            ProductCategories = ProductCategories
                        });
                    }
                }
                DBValues.ConnectDB.Close();
            }
            return values;
        }
    }
}
