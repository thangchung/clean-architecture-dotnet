rmdir /S /Q Data/Migrations

dotnet ef migrations add InitialProductionDb -c MainDbContext -o Data/Migrations
