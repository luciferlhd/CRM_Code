using Volo.Abp.Modularity;

namespace CRM_Code;

[DependsOn(
    typeof(CRM_CodeApplicationModule),
    typeof(CRM_CodeDomainTestModule)
    )]
public class CRM_CodeApplicationTestModule : AbpModule
{

}
