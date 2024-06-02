using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Tour_Planner.Services.PdfReportGenerationServices;

public interface IPdfReportGenerationService {
    Task GenerateOneTourReport(Tour tour, string path);
    void GenerateToursSummaryReport(List<Tour> tours, string path);
}