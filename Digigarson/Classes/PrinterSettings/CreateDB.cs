using System.Data.SQLite;
using System.IO;

namespace Digigarson.Classes.PrinterSettings
{
    class CreateDB
    {
        private Values DBValues = new Values();
        private string DBVersion { get; set; }
        public CreateDB(string DBVersion) {
            this.DBVersion = DBVersion;
        }
        // Create Database
        public void Create() {
            checkDB();
        }
        // Check Database Location and Database Version
        private void checkDB() {
            if (Directory.Exists(DBValues.FolderLocation)) {
                if (!File.Exists(DBValues.FolderLocation + DBValues.DBName)) {
                    createDBFile(DBValues.FolderLocation + DBValues.DBName);
                } else {
                    if (this.DBVersion != DBVersion) {
                        setUpdateDBValues();
                    }
                }
            } else {
                Directory.CreateDirectory(DBValues.FolderLocation);
                createDBFile(DBValues.FolderLocation + DBValues.DBName);
            }
            createTables();
        }
        // Create DataBase File
        private void createDBFile(string DBFileLocation) {
            SQLiteConnection.CreateFile(DBFileLocation);
        }
        // Set Update New Columns, Tables and DB
        private void setUpdateDBValues() {
            // Dont Delete
            /*using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                SELECT COUNT(*) AS CNTREC FROM pragma_table_info('tablename') WHERE name='column_name'
            }*/
        }
        // Create Tables (Check Exist)
        private void createTables() {
            using (DBValues.ConnectDB = new SQLiteConnection("Data Source = " + DBValues.FolderLocation + DBValues.DBName + "; Version = 3;")) {
                DBValues.ConnectDB.Open();
                // Group
                using (SQLiteCommand Group = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    Group.CommandText = "CREATE TABLE IF NOT EXISTS 'groups'("
                        + "'group_id' TEXT PRIMARY KEY,"
                        + "'group_name' TEXT,"
                        + "'printer_name' TEXT"
                    + ")";
                    Group.ExecuteNonQuery();
                }
                // Group Product Categories
                using (SQLiteCommand GroupProductCategories = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    GroupProductCategories.CommandText = "CREATE TABLE IF NOT EXISTS 'group_product_categories'("
                        + "'group_id' TEXT,"
                        + "'product_category_id' TEXT"
                    + ")";
                    GroupProductCategories.ExecuteNonQuery();
                }
                // Group Products
                using (SQLiteCommand GroupProducts = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    GroupProducts.CommandText = "CREATE TABLE IF NOT EXISTS 'group_removed_products'("
                        + "'group_id' TEXT,"
                        + "'product_category_id' TEXT,"
                        + "'product_id' TEXT"
                    + ")";
                    GroupProducts.ExecuteNonQuery();
                }
                // Options
                using (SQLiteCommand Options = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    Options.CommandText = "CREATE TABLE IF NOT EXISTS 'options'("
                        + "'option_name' TEXT," // Maybe id
                        + "'value' TEXT"
                    + ")";
                    Options.ExecuteNonQuery();
                }
                // Options Content
                using (SQLiteCommand OptionsContent = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    OptionsContent.CommandText = "CREATE TABLE IF NOT EXISTS 'options_content'("
                        + "'option_name' TEXT," // Maybe id
                        + "'option_content_name' TEXT,"
                        + "'value' TEXT"
                    + ")";
                    OptionsContent.ExecuteNonQuery();
                }
                // Saved Orders
                using (SQLiteCommand SavedOrders = new SQLiteCommand(null, DBValues.ConnectDB)) {
                    SavedOrders.CommandText = "CREATE TABLE IF NOT EXISTS 'orders'("
                        + "'order_id' TEXT"
                    + ")";
                    SavedOrders.ExecuteNonQuery();
                }
                DBValues.ConnectDB.Close();
            }
        }
    }
}
