using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using CRM_Code.Books;
using CRM_Code.Shared;

namespace CRM_Code.Web.Pages.Books
{
    public class IndexModel : AbpPageModel
    {
        public string TitleFilter { get; set; }
        public int? PageCountFilterMin { get; set; }

        public int? PageCountFilterMax { get; set; }
        public float? PirceFilterMin { get; set; }

        public float? PirceFilterMax { get; set; }
        [SelectItems(nameof(AuthorLookupList))]
        public Guid? AuthorIdFilter { get; set; }
        public List<SelectListItem> AuthorLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        private readonly IBooksAppService _booksAppService;

        public IndexModel(IBooksAppService booksAppService)
        {
            _booksAppService = booksAppService;
        }

        public async Task OnGetAsync()
        {
            AuthorLookupList.AddRange((
                    await _booksAppService.GetAuthorLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            await Task.CompletedTask;
        }
    }
}