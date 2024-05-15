﻿using DataAccessLayer.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;

namespace DataAccessLayer;

public class TourPlannerDbContext : DbContext
{
    /*private readonly IConfiguration _configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();*/
    public TourPlannerDbContext() {}
    
    public TourPlannerDbContext(DbContextOptions<TourPlannerDbContext> options) : base(options) {}
    
    public DbSet<TourDTO> Tours { get; set; }
    
    /*public void EnsureDb()
    {
        try
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }*/
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TourPlanner;Username=postgres;Password=postgres");
    }
}
