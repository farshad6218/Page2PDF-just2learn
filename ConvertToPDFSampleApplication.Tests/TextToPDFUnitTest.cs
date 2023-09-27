
using ConvertToPDFSampleApplication;
using DinkToPdf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

[TestClass]
public class TextToPDFUnitTest
{
    [TestMethod]
    public void TextToPDFbypdfShart_ShouldGeneratePDF()
    {
        // Arrange
        string inputTextPath = "input.txt"; // Path to your input text file
        string outputFileName = "output.pdf";
        string fontName = "Arial";
        int fontSize = 12;

        var mockHttpContext = new Mock<System.Web.HttpContextBase>();
        var mockHttpResponse = new Mock<System.Web.HttpResponseBase>();

        // Set up the mock HttpContext to return the mock HttpResponse
        mockHttpContext.Setup(ctx => ctx.Response).Returns(mockHttpResponse.Object);

        // Act
        TextToPDFClass.TextToPDFbypdfShart(inputTextPath, outputFileName, fontName, fontSize);

        // Assert
        mockHttpResponse.VerifySet(r => r.ContentType = "application/pdf");
        mockHttpResponse.Verify(r => r.AddHeader("content-disposition", "attachment;filename=output.pdf"), Times.Once); // Verify that the output filename is set correctly

    }


    [TestMethod]
    public void HtmlToPdfByDink_ConvertsHtmlToPdf()
    {
        // Arrange
        string inputTextPath = "<html><head></head><body><div>Sample HTML File</div></body></html>"; // OR Replace with the path to your HTML input file
        string outputFileName = "output.pdf";
        double? topMargin = 10;
        double? rightMargin = 10;
        double? leftMargin = 10;
        double? bottomMargin = 10;
        PaperKind paperSize = PaperKind.A4;
        Orientation pageOrientation = Orientation.Portrait;
        int pageOffset = 0;

        var mockHttpContext = new Mock<System.Web.HttpContextBase>();
        var mockHttpResponse = new Mock<System.Web.HttpResponseBase>();

        // Set up the mock HttpContext to return the mock HttpResponse
        mockHttpContext.Setup(ctx => ctx.Response).Returns(mockHttpResponse.Object);

        // Act
        TextToPDFClass.HtmlToPdfByDink(inputTextPath, outputFileName, topMargin, rightMargin, leftMargin, bottomMargin, paperSize, pageOrientation, pageOffset);

        // Assert
        mockHttpResponse.VerifySet(r => r.ContentType = "application/pdf");
        mockHttpResponse.Verify(r => r.AddHeader("content-disposition", "attachment;filename=output.pdf"), Times.Once); // Verify that the output filename is set correctly
    }
}
