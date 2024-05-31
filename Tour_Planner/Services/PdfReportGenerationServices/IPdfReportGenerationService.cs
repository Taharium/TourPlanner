using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Wpf;
using Models;

namespace Tour_Planner.Services.PdfReportGenerationServices;

public interface IPdfReportGenerationService {
    void GenerateOneTourReport(Tour tour, string path);
    void GenerateToursSummaryReport(List<Tour> tours, string path);
    Task CaptureWebView2(WebView2 webView2, string imagePath);
}