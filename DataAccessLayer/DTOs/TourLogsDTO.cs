using System.ComponentModel.DataAnnotations.Schema;
using Models.Enums;

namespace DataAccessLayer.DTOs;

public class TourLogsDTO
{
    public int Id { get; set; }
    //public int TourId { get; set; }
    public TourDTO Tour { get; set; } = null!;
    public DateTime DateTime { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string TotalTime { get; set; } = null!;
    [Column(TypeName = "varchar(100)")]
    public string Distance { get; set; } = null!;
    [Column(TypeName = "varchar(250)")]
    public string Comment { get; set; } = null!;
    public Difficulty Difficulty { get; set; }
    public Rating Rating { get; set; }

}