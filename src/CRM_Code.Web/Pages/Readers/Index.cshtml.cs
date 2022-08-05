using CRM_Code.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using CRM_Code.Readers;
using CRM_Code.Shared;

namespace CRM_Code.Web.Pages.Readers
{
    public class IndexModel : AbpPageModel
    {
        public string NameSurnameFilter { get; set; }
        public string EmailAddressFilter { get; set; }
        public Gender? GenderFilter { get; set; }

        private readonly IReadersAppService _readersAppService;

        public IndexModel(IReadersAppService readersAppService)
        {
            _readersAppService = readersAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}