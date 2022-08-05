using CRM_Code.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using CRM_Code.Books;

namespace CRM_Code.Web.Pages.Books
{
    public class EditModalModel : CRM_CodePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public BookUpdateDto Book { get; set; }

        public List<SelectListItem> AuthorLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IBooksAppService _booksAppService;

        public EditModalModel(IBooksAppService booksAppService)
        {
            _booksAppService = booksAppService;
        }

        public async Task OnGetAsync()
        {
            var bookWithNavigationPropertiesDto = await _booksAppService.GetWithNavigationPropertiesAsync(Id);
            Book = ObjectMapper.Map<BookDto, BookUpdateDto>(bookWithNavigationPropertiesDto.Book);

            AuthorLookupList.AddRange((
                                    await _booksAppService.GetAuthorLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _booksAppService.UpdateAsync(Id, Book);
            return NoContent();
        }
    }
}