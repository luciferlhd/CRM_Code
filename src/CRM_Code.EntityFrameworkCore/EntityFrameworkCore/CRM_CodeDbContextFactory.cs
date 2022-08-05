using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CRM_Code.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class CRM_CodeDbContextFactory : IDesignTimeDbContextFactory<CRM_CodeDbContext>
{
    public CRM_CodeDbContext CreateDbContext(string[] args)
    {
        CRM_CodeEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<CRM_CodeDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new CRM_CodeDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../CRM_Code.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
