using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TheArtOfDev.HtmlRenderer.Core.Entities;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace Digigarson.Classes.PrinterSettings
{
    class ConvertHTMLToPDF
    {

        public void Convert(string pdfPath, string htmlHead, string htmlBody, double invoiceHeight, double invoiceWidth) {
            
            var html = ""
                + "<html lang='tr'>"
                    + "<head>"
                        + "<meta charset='UTF-8'>"
                    + "</head>"
                    + "<body>"
                        + htmlBody
                    + "</body>"
                + "</html>";
            try {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Invoice/")) {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Invoice/");
                }

                using (FileStream fs = File.Create(AppDomain.CurrentDomain.BaseDirectory + "/Invoice/Invoice.html")) {
                    // Add some text to file
                    Byte[] title = new UTF8Encoding(true).GetBytes("<style>" + htmlHead + "</style>" + htmlBody);
                    fs.Write(title, 0, title.Length);
                }

                var cssData = PdfGenerator.ParseStyleSheet(htmlHead, true);
                //PdfGenerator.AddFontFamilyMapping
                var config = new PdfGenerateConfig();
                config.PageOrientation = PageOrientation.Landscape;
                config.ManualPageSize = new XSize(XUnit.FromMillimeter(invoiceHeight), XUnit.FromCentimeter(invoiceWidth));
                using (PdfDocument document = PdfGenerator.GeneratePdf(html, config, cssData, null)) {
                    document.PageLayout = PdfPageLayout.SinglePage;
                    document.Save(pdfPath);
                    document.Close();
                }
            } catch (Exception exception) {
                MessageBox.Show("Sipariş PDF'e dönüştürülemediği için fiş çıkarılamadı." + exception.ToString(), "Dönüştürme Mesajı", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
        /*public static void OnImageLoadPdfSharp(object sender, HtmlImageLoadEventArgs e) {
            var imgObj = Image.FromFile(@"" + AppDomain.CurrentDomain.BaseDirectory + "/Images/1.png");
            e.Callback(XImage.FromGdiPlusImage(imgObj));
        }
        
       public static void OnStylesheetLoad(object sender, HtmlStylesheetLoadEventArgs e) {
            e.SetStyleSheet = @".class{}";
        }*/
    }
}
