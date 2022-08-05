using CRM_Code.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace CRM_Code.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CRM_CodeEntityFrameworkCoreModule),
    typeof(CRM_CodeApplicationContractsModule)
)]
public class CRM_CodeDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options =>
        {
            options.IsJobExecutionEnabled = false;
        });
    }
}
