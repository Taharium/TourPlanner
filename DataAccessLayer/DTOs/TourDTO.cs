using System.Collections;
using System.ComponentModel.DataAnnotations;
using Models;
using Models.Enums;

namespace DataAccessLayer.DTOs;

public class TourDTO
{
    public int Id { get; set; }
    [MaxLength(200)]
    public string Name { get; set; } = null!;
    [MaxLength(300)]
    public string Description { get; set; } = null!;
    [MaxLength(100)]
    public string StartLocation { get; set; } = null!;
    [MaxLength(100)]
    public string EndLocation { get; set; } = null!;
    [MaxLength(100)]
    public string Distance { get; set; } = null!;
    [MaxLength(100)]
    public string EstimatedTime { get; set; } = null!;
    public ICollection<TourLogsDTO> TourLogsList { get; set; } = [];
    public TransportType TransportType { get; set; } 
}