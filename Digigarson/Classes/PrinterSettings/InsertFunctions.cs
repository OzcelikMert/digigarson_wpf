using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Digigarson.Classes.PrinterSettings.Classes;

namespace Digigarson.Classes.PrinterSettings
{
    class InsertFunctions
    {
        Values DBValues = new Values();
        // Save New Group
        public void saveNewGroup(string GroupID, string GroupName, string PrinterName) {
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                // Group
                using (SQLiteCommand NewGroup = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    NewGroup.CommandText = "insert into 'groups'(" +
                        "group_id," +
                        "group_name," +
                        "printer_name" +
                    ") values" +
                    "(" +
                        "@group_id," +
                        "@group_name," +
                        "@printer_name" +
                    ")";
                    NewGroup.Parameters.AddWithValue("@group_id", GroupID);
                    NewGroup.Parameters.AddWithValue("@group_name", GroupName);
                    NewGroup.Parameters.AddWithValue("@printer_name", PrinterName);
                    NewGroup.ExecuteNonQuery();
                }
                DBValues.ConnectDB.Close();
            }
        }
        // Save New Group Product Categories
        public void saveNewGroupProductCategories(string GroupID, Array ProductCategories) {
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                // Group
                using (SQLiteCommand NewGroupProductCategories = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    NewGroupProductCategories.CommandText = "insert into 'group_product_categories'(" +
                        "group_id," +
                        "product_category_id" +
                    ") values";
                    int count = 0;
                    foreach (var ProductCategory in ProductCategories) {
                        string parameter_group_id = "@group_id_" + count.ToString();
                        string parameter_product_category_id = "@product_category_id_" + count.ToString();
                        NewGroupProductCategories.CommandText += "(" +
                            parameter_group_id + "," +
                            parameter_product_category_id + "" +
                        "),";
                        NewGroupProductCategories.Parameters.Add(new SQLiteParameter(parameter_group_id, GroupID));
                        NewGroupProductCategories.Parameters.Add(new SQLiteParameter(parameter_product_category_id, ProductCategory));
                        count++;
                    }
                    if (count > 0) {
                        // Convert (,) To (;)
                        NewGroupProductCategories.CommandText = NewGroupProductCategories.CommandText.Remove(NewGroupProductCategories.CommandText.Length - 1, 1);
                        NewGroupProductCategories.CommandText += ";";
                        NewGroupProductCategories.ExecuteNonQuery();
                    }
                }
                DBValues.ConnectDB.Close();
            }
        }
        // Save New Products
        public void saveNewGroupProducts(string GroupID, List<GroupProducts> GroupProducts) {
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                // Group
                using (SQLiteCommand NewProducts = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    NewProducts.CommandText = "insert into 'group_removed_products'(" +
                        "group_id," +
                        "product_category_id," +
                        "product_id" +
                    ") values";
                    int count = 0;
                    foreach (var GroupProduct in GroupProducts) {
                        if (GroupProduct.Products != null) {
                            foreach (var Product in GroupProduct.Products) {
                                string parameter_group_id = "@group_id_" + count.ToString();
                                string parameter_product_category_id = "@product_category_id_" + count.ToString();
                                string parameter_product_id = "@product_id_" + count.ToString();
                                NewProducts.CommandText += "(" +
                                    parameter_group_id + "," +
                                    parameter_product_category_id + "," +
                                    parameter_product_id + "" +
                                "),";
                                NewProducts.Parameters.Add(new SQLiteParameter(parameter_group_id, GroupID));
                                NewProducts.Parameters.Add(new SQLiteParameter(parameter_product_category_id, GroupProduct.ProductCategory));
                                NewProducts.Parameters.Add(new SQLiteParameter(parameter_product_id, Product));
                                count++;
                            }
                        }
                    }
                    if (count > 0) {
                        // Convert (,) To (;)
                        NewProducts.CommandText = NewProducts.CommandText.Remove(NewProducts.CommandText.Length - 1, 1);
                        NewProducts.CommandText += ";";
                        NewProducts.ExecuteNonQuery();
                    }
                }
                DBValues.ConnectDB.Close();
            }
        }
        // Save Options
        public void savePrinterOptions(List<PrinterOptions> PrinterOptions) {
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                // Group
                using (SQLiteCommand SavePrinterOptions = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    SavePrinterOptions.CommandText = "insert into 'options'(" +
                        "option_name," +
                        "value" +
                    ") values";
                    int count = 0;
                    foreach (var PrinterOption in PrinterOptions) {
                        string parameter_option_name = "@option_name_" + count.ToString();
                        string parameter_value = "@value_" + count.ToString();
                        SavePrinterOptions.CommandText += "(" +
                            parameter_option_name + "," +
                            parameter_value +
                        "),";
                        SavePrinterOptions.Parameters.Add(new SQLiteParameter(parameter_option_name, PrinterOption.Name));
                        SavePrinterOptions.Parameters.Add(new SQLiteParameter(parameter_value, PrinterOption.Value));
                        count++;
                    }
                    if (count > 0) {
                        // Convert (,) To (;)
                        SavePrinterOptions.CommandText = SavePrinterOptions.CommandText.Remove(SavePrinterOptions.CommandText.Length - 1, 1);
                        SavePrinterOptions.CommandText += ";";
                        SavePrinterOptions.ExecuteNonQuery();
                    }
                }
                DBValues.ConnectDB.Close();
            }
        }
        // Save Options Contents
        public void savePrinterOptionsContents(List<PrinterOptions> PrinterOptions) {
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                // Group
                using (SQLiteCommand SavePrinterOptionsContent = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    SavePrinterOptionsContent.CommandText = "insert into 'options_content'(" +
                        "option_name," +
                        "option_content_name," +
                        "value" +
                    ") values";
                    int count = 0;
                    foreach (var PrinterOption in PrinterOptions) {
                        List<Contents> Contents = PrinterOption.Contents;
                            foreach (var Content in Contents){
                                if(Content.Name.Length > 0) { 
                                    string parameter_option_name = "@option_name_" + count.ToString();
                                    string parameter_option_content_name = "@option_content_name_" + count.ToString();
                                    string parameter_value = "@value_" + count.ToString();
                                    SavePrinterOptionsContent.CommandText += "(" +
                                        parameter_option_name + "," +
                                        parameter_option_content_name + "," +
                                        parameter_value +
                                    "),";
                                    SavePrinterOptionsContent.Parameters.Add(new SQLiteParameter(parameter_option_name, PrinterOption.Name));
                                    SavePrinterOptionsContent.Parameters.Add(new SQLiteParameter(parameter_option_content_name, Content.Name));
                                    SavePrinterOptionsContent.Parameters.Add(new SQLiteParameter(parameter_value, Content.Value));
                                    count++;
                                }
                            }
                    }
                    if (count > 0) {
                        // Convert (,) To (;)
                        SavePrinterOptionsContent.CommandText = SavePrinterOptionsContent.CommandText.Remove(SavePrinterOptionsContent.CommandText.Length - 1, 1);
                        SavePrinterOptionsContent.CommandText += ";";
                        SavePrinterOptionsContent.ExecuteNonQuery();
                    }
                }
                DBValues.ConnectDB.Close();
            }
        }
    }
}
