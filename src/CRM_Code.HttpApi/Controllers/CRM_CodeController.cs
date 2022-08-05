using CRM_Code.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CRM_Code.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class CRM_CodeController : AbpControllerBase
{
    protected CRM_CodeController()
    {
        LocalizationResource = typeof(CRM_CodeResource);
    }
}
