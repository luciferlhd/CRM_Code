using CRM_Code.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using CRM_Code.Authors;

namespace CRM_Code.Web.Pages.Authors
{
    public class EditModalModel : CRM_CodePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public AuthorUpdateDto Author { get; set; }

        private readonly IAuthorsAppService _authorsAppService;

        public EditModalModel(IAuthorsAppService authorsAppService)
        {
            _authorsAppService = authorsAppService;
        }

        public async Task OnGetAsync()
        {
            var author = await _authorsAppService.GetAsync(Id);
            Author = ObjectMapper.Map<AuthorDto, AuthorUpdateDto>(author);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _authorsAppService.UpdateAsync(Id, Author);
            return NoContent();
        }
    }
}