﻿using DataAccessLayer.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;

namespace DataAccessLayer;

public class TourPlannerDbContext : DbContext
{
    /*private readonly IConfiguration _configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();*/

    /*private readonly IConfigDatabase _configuration;*/
    
    public TourPlannerDbContext(DbContextOptions<TourPlannerDbContext> options) : base(options) {}

    /*public TourPlannerDbContext()
    {
    }*/
    
    
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
    
    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        optionsBuilder.UseNpgsql(_configuration.ConnectionStringDb);
    }*/
}
