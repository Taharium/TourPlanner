using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Models.Enums;

namespace DataAccessLayer.DTOs;

public class TourDTO
{
    public int Id { get; set; } = 1;
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; } = null!;
    [Column(TypeName = "varchar(250)")]
    public string Description { get; set; } = null!;
    [Column(TypeName = "varchar(100)")]
    public string StartLocation { get; set; } = null!;
    [Column(TypeName = "varchar(100)")]
    public string EndLocation { get; set; } = null!;
    [Column(TypeName = "varchar(100)")]
    public string Distance { get; set; } = null!;
    [Column(TypeName = "varchar(100)")]
    public string EstimatedTime { get; set; } = null!;
    public TransportType TransportType { get; set; } 
    public Popularity Popularity { get; set; }
    public Child_Friendliness ChildFriendliness { get; set; }
    [Column(TypeName = "Text")]
    public string Directions { get; set; } = null!;
    public ICollection<TourLogsDTO> TourLogsList { get; set; } = new List<TourLogsDTO>();
}