using CRM_Code.Shared;
using CRM_Code.Books;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using CRM_Code.Readers;

namespace CRM_Code.Web.Pages.Readers
{
    public class EditModalModel : CRM_CodePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public ReaderUpdateDto Reader { get; set; }

        public List<BookDto> Books { get; set; }
        [BindProperty]
        public List<Guid> SelectedBookIds { get; set; }

        private readonly IReadersAppService _readersAppService;

        public EditModalModel(IReadersAppService readersAppService)
        {
            _readersAppService = readersAppService;
        }

        public async Task OnGetAsync()
        {
            var readerWithNavigationPropertiesDto = await _readersAppService.GetWithNavigationPropertiesAsync(Id);
            Reader = ObjectMapper.Map<ReaderDto, ReaderUpdateDto>(readerWithNavigationPropertiesDto.Reader);

            Books = readerWithNavigationPropertiesDto.Books;

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            Reader.BookIds = SelectedBookIds;

            await _readersAppService.UpdateAsync(Id, Reader);
            return NoContent();
        }
    }
}