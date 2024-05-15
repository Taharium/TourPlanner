using System.Collections;
using Models;
using Tour_Planner.Enums;

namespace DataAccessLayer.DTOs;

public class TourDTO
{
    public int Id { get; set; }
    /*public ICollection<TourLogsDTO> TourLogsList { get; set; } = [];*/
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string StartLocation { get; set; } = null!;
    public string EndLocation { get; set; } = null!;
    public TransportType TransportType { get; set; } 
    public string RouteInformationImage { get; set; } = null!;
    public string Distance { get; set; } = null!;
    public string EstimatedTime { get; set; } = null!;
}