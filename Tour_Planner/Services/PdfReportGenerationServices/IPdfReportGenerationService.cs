using System.Collections.Generic;
using Models;

namespace Tour_Planner.Services.PdfReportGenerationServices;

public interface IPdfReportGenerationService {
    void GenerateOneTourReport(Tour tour, string path);
    void GenerateToursSummaryReport(List<Tour> tours, string path);
}