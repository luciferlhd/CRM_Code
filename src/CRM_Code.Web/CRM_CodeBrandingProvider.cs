using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace CRM_Code.Web;

[Dependency(ReplaceServices = true)]
public class CRM_CodeBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "CRM_Code";
}
