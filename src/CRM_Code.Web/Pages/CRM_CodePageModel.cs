using CRM_Code.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace CRM_Code.Web.Pages;

public abstract class CRM_CodePageModel : AbpPageModel
{
    protected CRM_CodePageModel()
    {
        LocalizationResourceType = typeof(CRM_CodeResource);
    }
}
