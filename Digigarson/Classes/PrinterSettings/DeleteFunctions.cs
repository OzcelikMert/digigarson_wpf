using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using Digigarson.Classes.PrinterSettings.Classes;

namespace Digigarson.Classes.PrinterSettings
{
    class DeleteFunctions
    {
        Values DBValues = new Values();
        // Delete Group
        public void deleteGroup(string GroupID) {
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                using (SQLiteCommand DeleteGroup = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    DeleteGroup.CommandText = "delete from groups where group_id=@group_id";
                    DeleteGroup.Parameters.AddWithValue("@group_id", GroupID);
                    DeleteGroup.ExecuteNonQuery();
                }
                DBValues.ConnectDB.Close();
            }
        }
        // Delete Group Product Categories
        public void deleteGroupProductCategories(string GroupID) {
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                using (SQLiteCommand DeleteGroupProductCategories = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    DeleteGroupProductCategories.CommandText = "delete from group_product_categories where group_id=@group_id";
                    DeleteGroupProductCategories.Parameters.AddWithValue("@group_id", GroupID);
                    DeleteGroupProductCategories.ExecuteNonQuery();
                }
                DBValues.ConnectDB.Close();
            }
        }
        public void deleteGroupProductCategories(string GroupID, Array ProductCategories) {
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                using (SQLiteCommand DeleteGroupProductCategories = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    DeleteGroupProductCategories.CommandText = "delete from group_product_categories where ";
                    int count = 0;
                    foreach (var ProductCategory in ProductCategories) {
                        string parameter_group_id = "@group_id_" + count.ToString();
                        string parameter_product_category_id = "@product_category_id_" + count.ToString();
                        DeleteGroupProductCategories.CommandText += " (group_id=" + parameter_group_id + " and product_category_id=" + parameter_product_category_id + ") or";
                        DeleteGroupProductCategories.Parameters.AddWithValue("@group_id", GroupID);
                        DeleteGroupProductCategories.Parameters.AddWithValue("@product_category_id", ProductCategories);
                        DeleteGroupProductCategories.Parameters.Add(new SQLiteParameter(parameter_group_id, GroupID));
                        DeleteGroupProductCategories.Parameters.Add(new SQLiteParameter(parameter_product_category_id, ProductCategory));
                        count++;
                    }
                    if (count > 0) {
                        DeleteGroupProductCategories.CommandText = DeleteGroupProductCategories.CommandText.Remove(DeleteGroupProductCategories.CommandText.Length - 2, 2);
                        DeleteGroupProductCategories.ExecuteNonQuery();
                    }
                }
                DBValues.ConnectDB.Close();
            }
        }
        // Delete Group Category Products
        public void deleteGroupCategoryProducts(string GroupID) {
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                using (SQLiteCommand DeleteGroupCategoryProducts = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    DeleteGroupCategoryProducts.CommandText = "delete from group_removed_products where group_id=@group_id";
                    DeleteGroupCategoryProducts.Parameters.AddWithValue("@group_id", GroupID);
                    DeleteGroupCategoryProducts.ExecuteNonQuery();
                }
                DBValues.ConnectDB.Close();
            }
        }
        public void deleteGroupCategoryProducts(string GroupID, Array ProductCategories) {
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                using (SQLiteCommand DeleteGroupCategoryProducts = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    DeleteGroupCategoryProducts.CommandText = "delete from group_removed_products where ";
                    int count = 0;
                    foreach (var ProductCategory in ProductCategories) {
                        string parameter_group_id = "@group_id_" + count.ToString();
                        string parameter_product_category_id = "@product_category_id_" + count.ToString();
                        DeleteGroupCategoryProducts.CommandText += " (group_id=" + parameter_group_id + " and product_category_id=" + parameter_product_category_id + ") or";
                        DeleteGroupCategoryProducts.Parameters.AddWithValue("@group_id", GroupID);
                        DeleteGroupCategoryProducts.Parameters.AddWithValue("@product_category_id", ProductCategories);
                        DeleteGroupCategoryProducts.Parameters.Add(new SQLiteParameter(parameter_group_id, GroupID));
                        DeleteGroupCategoryProducts.Parameters.Add(new SQLiteParameter(parameter_product_category_id, ProductCategory));
                        count++;
                    }
                    if (count > 0) {
                        DeleteGroupCategoryProducts.CommandText = DeleteGroupCategoryProducts.CommandText.Remove(DeleteGroupCategoryProducts.CommandText.Length - 2, 2);
                        DeleteGroupCategoryProducts.ExecuteNonQuery();
                    }
                }
                DBValues.ConnectDB.Close();
            }
        }
        public void deleteGroupCategoryProducts(string GroupID, List<GroupProducts> GroupProducts) {
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                using (SQLiteCommand DeleteGroupCategoryProducts = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    DeleteGroupCategoryProducts.CommandText = "delete from group_removed_products where ";
                    int count = 0;
                    foreach (var GroupProduct in GroupProducts) {
                        string parameter_group_id = "@group_id_" + count.ToString();
                        string parameter_product_category_id = "@product_category_id_" + count.ToString();
                        DeleteGroupCategoryProducts.CommandText += " (group_id=" + parameter_group_id + " and product_category_id=" + parameter_product_category_id + ") or";
                        DeleteGroupCategoryProducts.Parameters.Add(new SQLiteParameter(parameter_group_id, GroupID));
                        DeleteGroupCategoryProducts.Parameters.Add(new SQLiteParameter(parameter_product_category_id, GroupProduct.ProductCategory));
                        count++;
                    }
                    if (count > 0) {
                        DeleteGroupCategoryProducts.CommandText = DeleteGroupCategoryProducts.CommandText.Remove(DeleteGroupCategoryProducts.CommandText.Length - 2, 2);
                        DeleteGroupCategoryProducts.ExecuteNonQuery();
                    }
                    DeleteGroupCategoryProducts.ExecuteNonQuery();
                }
                DBValues.ConnectDB.Close();
            }
        }
        // Delete Options
        public void deletePrinterOptions() {
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                // Group
                using (SQLiteCommand DeletePrinterOptions = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    DeletePrinterOptions.CommandText = "delete from options";
                    DeletePrinterOptions.ExecuteNonQuery();
                }
                DBValues.ConnectDB.Close();
            }
        }
        // Delete Options Contents
        public void deletePrinterOptionsContents() {
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                // Group
                using (SQLiteCommand DeletePrinterOptionsContent = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    DeletePrinterOptionsContent.CommandText = "delete from options_content";
                    DeletePrinterOptionsContent.ExecuteNonQuery();
                }
                DBValues.ConnectDB.Close();
            }
        }
    }
}
