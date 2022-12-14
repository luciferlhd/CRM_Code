using CRM_Code.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRM_Code.Books;

namespace CRM_Code.Web.Pages.Books
{
    public class CreateModalModel : CRM_CodePageModel
    {
        [BindProperty]
        public BookCreateDto Book { get; set; }

        public List<SelectListItem> AuthorLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IBooksAppService _booksAppService;

        public CreateModalModel(IBooksAppService booksAppService)
        {
            _booksAppService = booksAppService;
        }

        public async Task OnGetAsync()
        {
            Book = new BookCreateDto();
            AuthorLookupList.AddRange((
                                    await _booksAppService.GetAuthorLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _booksAppService.CreateAsync(Book);
            return NoContent();
        }
    }
}