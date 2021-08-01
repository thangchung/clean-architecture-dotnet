rmdir /S /Q Data/CompiledModels

dotnet ef dbcontext optimize -c MainDbContext -o Data/CompiledModels -n SettingService.Infrastructure
