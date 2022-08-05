using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using CRM_Code.Authors;
using CRM_Code.Shared;

namespace CRM_Code.Web.Pages.Authors
{
    public class IndexModel : AbpPageModel
    {
        public string NameFilter { get; set; }
        public DateTime? BirthdateFilterMin { get; set; }

        public DateTime? BirthdateFilterMax { get; set; }
        [SelectItems(nameof(ActiveBoolFilterItems))]
        public string ActiveFilter { get; set; }

        public List<SelectListItem> ActiveBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };

        private readonly IAuthorsAppService _authorsAppService;

        public IndexModel(IAuthorsAppService authorsAppService)
        {
            _authorsAppService = authorsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}