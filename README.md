# Tour_Planner

An application to create, add, remove and update Tours.

To use our application an appsettings.json is needed.
It should look like this
```
{
  "ConnectionStrings": {
    "DataBase": "Include Error Detail=True;Host=localhost;Database=Tour_Planner;Username=postgres;Password=postgres",
    "API-Key": "APIKEY",
    "Weather-API-Key": "APIKEY"
  }
}
```

## For migration
```
dotnet ef database update --verbose --project . --startup-project ..\Tour_Planner\
```