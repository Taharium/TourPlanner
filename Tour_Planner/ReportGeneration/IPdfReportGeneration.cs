using Models;

namespace Tour_Planner.ReportGeneration;

public interface IPdfReportGeneration {
    void GenerateReport(Tour tour, string path);
}