using CRM_Code.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace CRM_Code;

[DependsOn(
    typeof(CRM_CodeEntityFrameworkCoreTestModule)
    )]
public class CRM_CodeDomainTestModule : AbpModule
{

}
