using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Tour_Planner.Enums;

namespace DataAccessLayer.DTOs;

public class TourLogsDTO
{
    public int Id { get; set; }
    public int TourId { get; set; }
    public TourDTO Tour { get; set; } = null!;
    public DateTime DateTime = DateTime.Now;
    [MaxLength(100)]
    public string TotalTime { get; set; } = null!;
    [MaxLength(100)]
    public string Distance { get; set; } = null!;
    [MaxLength(300)]
    public string Comment { get; set; } = null!;
    public Difficulty Difficulty { get; set; }
    public Rating Rating { get; set; }

}