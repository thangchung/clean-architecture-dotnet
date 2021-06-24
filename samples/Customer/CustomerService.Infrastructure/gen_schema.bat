rmdir /S /Q Data/Migrations

dotnet ef migrations add InitialCustomerDb -c MainDbContext -o Data/Migrations
