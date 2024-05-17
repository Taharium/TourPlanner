using DataAccessLayer;
using Microsoft.Extensions.Configuration;

namespace Tour_Planner.Configurations;

public class AppConfiguration : IConfigDatabase {

    private IConfiguration _configuration;
    
    public AppConfiguration(IConfiguration configuration) {
        _configuration = configuration;
    }

    public string ConnectionStringDb => _configuration["ConnectionStrings:DataBase"]!;
}