using CRM_Code.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRM_Code.Readers;

namespace CRM_Code.Web.Pages.Readers
{
    public class CreateModalModel : CRM_CodePageModel
    {
        [BindProperty]
        public ReaderCreateDto Reader { get; set; }

        [BindProperty]
        public List<Guid> SelectedBookIds { get; set; }

        private readonly IReadersAppService _readersAppService;

        public CreateModalModel(IReadersAppService readersAppService)
        {
            _readersAppService = readersAppService;
        }

        public async Task OnGetAsync()
        {
            Reader = new ReaderCreateDto();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            Reader.BookIds = SelectedBookIds;

            await _readersAppService.CreateAsync(Reader);
            return NoContent();
        }
    }
}