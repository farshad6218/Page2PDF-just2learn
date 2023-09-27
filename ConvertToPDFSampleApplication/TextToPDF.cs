//Install-Package PdfSharp
//Install-Package DinkToPdf
//Install-Package DinkToPdfCopyDependencies -Version 1.0.8

using System.IO;

using System.Web.UI;

using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;

using DinkToPdf;
using DinkToPdf.Contracts;

 

namespace ConvertToPDFSampleApplication
{
    public class TextToPDFClass : Page
    {
        public static void TextToPDFbypdfShart(string inputTextPath, string outputFileName, string fontName, int fontSize)
        {
            string text = ReadTextFromFile(inputTextPath);

            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont(fontName, fontSize);
            XStringFormat format = new XStringFormat();
            format.LineAlignment = XLineAlignment.Near;

            // Create a PDF text object and draw the text on the page
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.DrawString(text, font, XBrushes.Black, new XRect(50, 50, page.Width - 100, page.Height - 100), format);

            // Set response headers for PDF file
            System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + outputFileName + ".pdf");


            // Stream the PDF document to the response
            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream, false);
                System.Web.HttpContext.Current.Response.BinaryWrite(stream.ToArray());
            }

            System.Web.HttpContext.Current.Response.End();

        }

        static string ReadTextFromFile(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }




        //remember to use data-image convertor for alll img tags
        //sample > https://www.cssportal.com/image-to-data/

        public static void HtmlToPdfByDink(string inputTextPath, string outputFileName, double? topMargin, double? rightMargin, double? leftMargin, double? bottomMargin, PaperKind paperSize, Orientation PageOrientation, int pageOffset)
        {

            string htmlContent = ReadTextFromFile(inputTextPath);

            IConverter _converter = new SynchronizedConverter(new PdfTools());

            // Set response headers for PDF file
            System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + outputFileName + ".pdf");


            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                                PaperSize =  paperSize,
                                ColorMode = ColorMode.Color,
                                PageOffset = pageOffset,
                                Margins = new MarginSettings() { Top = topMargin, Right = rightMargin, Left = leftMargin , Bottom = bottomMargin},
                                Orientation = PageOrientation,
                                                    },
                Objects = {
                                new ObjectSettings
                                {
                                    PagesCount = true,
                                    HtmlContent = htmlContent,
                                    WebSettings = { DefaultEncoding = "utf-8" }
                                }
                            }
            };

            byte[] pdfBytes = _converter.Convert(doc);


            // Stream the PDF document to the response
            using (MemoryStream stream = new MemoryStream())
            {
                System.Web.HttpContext.Current.Response.BinaryWrite(pdfBytes);
            }

            System.Web.HttpContext.Current.Response.End();
        }

    }
}