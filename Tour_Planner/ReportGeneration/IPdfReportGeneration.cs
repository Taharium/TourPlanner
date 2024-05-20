using Models;

namespace Tour_Planner.ReportGeneration;

public interface IPdfReportGeneration {
    void GenerateOneTourReport(Tour tour, string path);
}