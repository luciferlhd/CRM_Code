using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CRM_Code.Data;
using Volo.Abp.DependencyInjection;

namespace CRM_Code.EntityFrameworkCore;

public class EntityFrameworkCoreCRM_CodeDbSchemaMigrator
    : ICRM_CodeDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreCRM_CodeDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the CRM_CodeDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<CRM_CodeDbContext>()
            .Database
            .MigrateAsync();
    }
}
