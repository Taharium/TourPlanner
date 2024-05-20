using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Models;

namespace Tour_Planner.ReportGeneration;

public class PdfReportGeneration : IPdfReportGeneration {
    //Save file dialog, import dialog, export dialog 
    public void GenerateOneTourReport(Tour tour, string path) {
        var writer = new PdfWriter(path);
        var pdf = new PdfDocument(writer);
        var document = new Document(pdf);
        document.Add(new Paragraph("Tour Name: " + tour.Name));
        document.Add(new Paragraph("Tour Description: " + tour.Description));
        document.Add(new Paragraph("Start Location: " + tour.StartLocation));
        document.Add(new Paragraph("End Location: " + tour.EndLocation));
        document.Add(new Paragraph("Distance: " + tour.Distance));
        document.Add(new Paragraph("Transport Type: " + tour.TransportType));
        document.Close();
    }
}