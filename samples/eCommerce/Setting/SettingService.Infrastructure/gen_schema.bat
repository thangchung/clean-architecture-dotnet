rmdir /S /Q Data/Migrations

dotnet ef migrations add InitialSettingDb -c MainDbContext -o Data/Migrations
