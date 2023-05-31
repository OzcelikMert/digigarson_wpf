using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Printing;
using System.Security.Principal;
using System.Windows;
using Spire.Pdf;
using Spire.Pdf.Print;

namespace Digigarson.Classes.PrinterSettings
{
    class PrinterFunctions
    {
        private FileControl.Write write { get; set; }

        public void PrintPDF(string PrinterName, string PDFPath) {
            using (WindowsImpersonationContext impersonationContext = WindowsIdentity.Impersonate(IntPtr.Zero)) {
                try {
                    List<string> printerNames = InstalledPrinters;
                    if (printerNames.IndexOf(PrinterName) < 0) {
                        MessageBox.Show("'"+PrinterName+"' isimli yazıcı bilgisayarınızda kayıtlı değil!", "Yazıcı Ayarları", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    PdfDocument pdfdocument = new PdfDocument();
                    pdfdocument.LoadFromFile(PDFPath);
                    pdfdocument.PrintSettings.PrinterName = PrinterName;

                    SizeF pageSize = pdfdocument.Pages[0].Size;
                    PaperSize paper = new PaperSize("Custom", ((int)pageSize.Width / 72 * 100 + 80), (int)pageSize.Height / 72 * 100);
                    paper.RawKind = (int)PaperKind.Custom;

                    pdfdocument.PrintSettings.PaperSize = paper;
                    pdfdocument.PrintSettings.SelectSinglePageLayout(PdfSinglePageScalingMode.ActualSize, false);

                    pdfdocument.PrintSettings.SetPaperMargins(0, 0, 0, 0);
                    pdfdocument.PrintDocument.OriginAtMargins = true;
                    pdfdocument.PrintSettings.Landscape = false;
                    pdfdocument.PageSettings.Orientation = PdfPageOrientation.Portrait;
                    pdfdocument.PrintSettings.PrintController = new StandardPrintController();
                    pdfdocument.Print();
                    pdfdocument.Dispose();
                } catch (Exception exception) {
                    write = new FileControl.Write(DateTime.Now.ToString("dd/MM/yyyy H:mm") + " : Error : " + exception.ToString());
                    write._Write();
                    MessageBox.Show("Fiş yazdırılamadı!\nYazıcı İsimi: " + PrinterName + "\nHata: " + exception.ToString(), "Yazırma İşlemi", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        public List<string> InstalledPrinters {
            get {
                return (from PrintQueue printer in new LocalPrintServer().GetPrintQueues(new[] { EnumeratedPrintQueueTypes.Local,
                EnumeratedPrintQueueTypes.Connections }).ToList()
                        select printer.Name).ToList();
            }
            set { }
        }
    }
}
