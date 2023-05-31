using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Windows;

namespace Digigarson.Classes.JSInterfaces
{
    class PrinterSettings
    {
        private Window window { get; set; }
        public PrinterSettings(Window _window) {
            this.window = _window;
        }
        // Delete Functions
        private Classes.PrinterSettings.DeleteFunctions deleteFunctions = new Classes.PrinterSettings.DeleteFunctions();
        // Insert Functions
        private Classes.PrinterSettings.InsertFunctions insertFunctions = new Classes.PrinterSettings.InsertFunctions();
        // Select Functions
        private Classes.PrinterSettings.SelectFunctions selectFunctions = new Classes.PrinterSettings.SelectFunctions();
        // Printer Functions
        private Classes.PrinterSettings.PrinterFunctions printerFunctions = new Classes.PrinterSettings.PrinterFunctions();
        // Log Write
        private FileControl.Write write { get; set; }
        // Save Product Group
        public void saveProductGroupValues(string GroupID, string GroupName, string PrinterName, Array ProductCategories, List<Classes.PrinterSettings.Classes.GroupProducts> GroupProducts) {
            try {
                window.Dispatcher.BeginInvoke(new Action(() => {
                    deleteFunctions.deleteGroup(GroupID);
                    insertFunctions.saveNewGroup(GroupID, GroupName, PrinterName);
                    if (ProductCategories != null && ProductCategories.Length > 0) {
                        insertFunctions.saveNewGroupProductCategories(GroupID, ProductCategories);
                    }
                    if (GroupProducts != null && GroupProducts.Count > 0) {
                        deleteFunctions.deleteGroupCategoryProducts(GroupID, GroupProducts);
                        insertFunctions.saveNewGroupProducts(GroupID, GroupProducts);
                    }
                }));
            } catch (Exception exception) {
                write = new FileControl.Write(DateTime.Now.ToString("dd/MM/yyyy H:mm") + " : Error : " + exception.ToString());
                write._Write();
                MessageBox.Show("Grup değerleri kayıt edilemedi", "Yazıcı Ayarları", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        // Save Printer Options
        public void savePrinterOptions(List<Classes.PrinterSettings.Classes.PrinterOptions> PrinterOptions) {
            try {
                window.Dispatcher.BeginInvoke(new Action(() => {
                    deleteFunctions.deletePrinterOptions();
                    deleteFunctions.deletePrinterOptionsContents();
                    insertFunctions.savePrinterOptions(PrinterOptions);
                    insertFunctions.savePrinterOptionsContents(PrinterOptions);
                }));
            } catch (Exception exception) {
                write = new FileControl.Write(DateTime.Now.ToString("dd/MM/yyyy H:mm") + " : Error : " + exception.ToString());
                write._Write();
                MessageBox.Show("Yazıcı ayarları kayıt edilemedi", "Yazıcı Ayarları", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        // Get Printers
        public string[] getPrinters() {
            List<string> values = new List<string>();
            try {
                values = printerFunctions.InstalledPrinters;
            } catch (Exception exception) {
                write = new FileControl.Write(DateTime.Now.ToString("dd/MM/yyyy H:mm") + " : Error : " + exception.ToString());
                write._Write();
                MessageBox.Show("Yazıcı değerleri çekilemedi.", "Yazıcı Ayarları", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return values.ToArray();
        }
        // Get Groups
        public string getGroups() {
            string values = "";
            try {
                List<Classes.PrinterSettings.Classes.Groups> Groups = selectFunctions.getGroups();
                values = new JavaScriptSerializer().Serialize(Groups);
            } catch (Exception exception) {
                write = new FileControl.Write(DateTime.Now.ToString("dd/MM/yyyy H:mm") + " : Error : " + exception.ToString());
                write._Write();
                MessageBox.Show("Grup değerleri çekilemedi", "Yazıcı Ayarları", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return values;
        }
        // Get Group Product Categories
        public string getGroupProductCategories(string GroupID) {
            string values = "";
            try {
                List<Classes.PrinterSettings.Classes.GroupProductCategories> GroupProductCategories = selectFunctions.getGroupProductCategories(GroupID);
                values = new JavaScriptSerializer().Serialize(GroupProductCategories);
            } catch (Exception exception) {
                write = new FileControl.Write(DateTime.Now.ToString("dd/MM/yyyy H:mm") + " : Error : " + exception.ToString());
                write._Write();
                MessageBox.Show("Kayıt edilen ürün kategorileri çekilemdei", "Yazıcı Ayarları", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return values;
        }
        // Get Group Categories Products
        public string[] getGroupCategoryProducts(string GroupID, string ProductCategoryID) {
            List<string> values = new List<string>();
            try {
                values = selectFunctions.getGroupCategoryProducts(GroupID, ProductCategoryID);
            } catch (Exception exception) {
                write = new FileControl.Write(DateTime.Now.ToString("dd/MM/yyyy H:mm") + " : Error : " + exception.ToString());
                write._Write();
                MessageBox.Show("Kayıt edilen kategori ürünleri çekilemedi", "Yazıcı Ayarları", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return values.ToArray();
        }
        // Get Printer Options
        public string getPrinterOptions() {
            string values = "";
            try {
                List<Classes.PrinterSettings.Classes.PrinterOptions> PrinterOptions = selectFunctions.getPrinterOptions();
                values = new JavaScriptSerializer().Serialize(PrinterOptions);
            } catch (Exception exception) {
                write = new FileControl.Write(DateTime.Now.ToString("dd/MM/yyyy H:mm") + " : Error : " + exception.ToString());
                write._Write();
                MessageBox.Show("Yazıcı seçenekleri çekilemedi.", "Yazıcı Ayarları", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return values;
        }
        // Delete Group
        public void deleteGroup(string GroupID) {
            try {
                deleteFunctions.deleteGroup(GroupID);
                deleteFunctions.deleteGroupProductCategories(GroupID);
                deleteFunctions.deleteGroupCategoryProducts(GroupID);
            } catch (Exception exception) {
                write = new FileControl.Write(DateTime.Now.ToString("dd/MM/yyyy H:mm") + " : Error : " + exception.ToString());
                write._Write();
                MessageBox.Show("Grup değerleri silinemedi.", "Yazıcı Ayarları", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        // Delete Group Product Category
        public void deleteGroupProductCategory(string GroupID, Array ProductCategories) {
            try {
                deleteFunctions.deleteGroupProductCategories(GroupID, ProductCategories);
                deleteFunctions.deleteGroupCategoryProducts(GroupID, ProductCategories);
            } catch (Exception exception) {
                write = new FileControl.Write(DateTime.Now.ToString("dd/MM/yyyy H:mm") + " : Error : " + exception.ToString());
                write._Write();
                MessageBox.Show("Grup kategorileri ve ürünleri silinemedi.", "Yazıcı Ayarları", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        // Delete Group Product Category
        public void deleteGroupProduct(string GroupID, List<Classes.PrinterSettings.Classes.GroupProducts> GroupProducts) {
            try {
                deleteFunctions.deleteGroupCategoryProducts(GroupID, GroupProducts);
            } catch (Exception exception) {
                write = new FileControl.Write(DateTime.Now.ToString("dd/MM/yyyy H:mm") + " : Error : " + exception.ToString());
                write._Write();
                MessageBox.Show("Grup kategori ürünleri silinemedi.", "Yazıcı Ayarları", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        // Print Orders
        public void printOrders(List<Classes.PrinterSettings.Classes.Orders> Orders) {
            try {
                window.Dispatcher.BeginInvoke(new Action(() => {
                    Classes.PrinterSettings.ConvertHTMLToPDF convertHTMLToPDF = new Classes.PrinterSettings.ConvertHTMLToPDF();
                    foreach (var Order in Orders) {
                        if (!string.IsNullOrEmpty(Order.PrinterName)) {
                            string PDFPath = AppDomain.CurrentDomain.BaseDirectory + "/Invoice/invoice.pdf";
                            convertHTMLToPDF.Convert(PDFPath, Order.InvoiceHTML.Head, Order.InvoiceHTML.Body, Order.InvoiceHeight, Order.InvoiceWidth);
                            printerFunctions.PrintPDF(Order.PrinterName, PDFPath);
                        }
                    }
                }));
            } catch (Exception exception) {
                write = new FileControl.Write(DateTime.Now.ToString("dd/MM/yyyy H:mm") + " : Error : " + exception.ToString());
                write._Write();
                MessageBox.Show("Yeni gelen sipariş yazılamadı.", "Yazıcı Ayarları", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public string getGroupsInfos() {
            string values = "";
            try {
                List<Classes.PrinterSettings.Classes.GroupsInfos> GroupsInfos = selectFunctions.getGroupsInfos();
                values = new JavaScriptSerializer().Serialize(GroupsInfos);
            } catch (Exception exception) {
                write = new FileControl.Write(DateTime.Now.ToString("dd/MM/yyyy H:mm") + " : Error : " + exception.ToString());
                write._Write();
                MessageBox.Show("Grup bilgileri alınamadı.", "Yazıcı Ayarları", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return values;
        }
    }
}
