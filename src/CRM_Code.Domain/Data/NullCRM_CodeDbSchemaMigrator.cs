using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace CRM_Code.Data;

/* This is used if database provider does't define
 * ICRM_CodeDbSchemaMigrator implementation.
 */
public class NullCRM_CodeDbSchemaMigrator : ICRM_CodeDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
