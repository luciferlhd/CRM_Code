using CRM_Code.Localization;
using Volo.Abp.Application.Services;

namespace CRM_Code;

/* Inherit your application services from this class.
 */
public abstract class CRM_CodeAppService : ApplicationService
{
    protected CRM_CodeAppService()
    {
        LocalizationResource = typeof(CRM_CodeResource);
    }
}
