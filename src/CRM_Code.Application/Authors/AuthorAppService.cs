using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using CRM_Code.Permissions;
using CRM_Code.Authors;

namespace CRM_Code.Authors
{

    [Authorize(CRM_CodePermissions.Authors.Default)]
    public class AuthorsAppService : ApplicationService, IAuthorsAppService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly AuthorManager _authorManager;

        public AuthorsAppService(IAuthorRepository authorRepository, AuthorManager authorManager)
        {
            _authorRepository = authorRepository;
            _authorManager = authorManager;
        }

        public virtual async Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorsInput input)
        {
            var totalCount = await _authorRepository.GetCountAsync(input.FilterText, input.Name, input.BirthdateMin, input.BirthdateMax, input.Active);
            var items = await _authorRepository.GetListAsync(input.FilterText, input.Name, input.BirthdateMin, input.BirthdateMax, input.Active, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AuthorDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Author>, List<AuthorDto>>(items)
            };
        }

        public virtual async Task<AuthorDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Author, AuthorDto>(await _authorRepository.GetAsync(id));
        }

        [Authorize(CRM_CodePermissions.Authors.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _authorRepository.DeleteAsync(id);
        }

        [Authorize(CRM_CodePermissions.Authors.Create)]
        public virtual async Task<AuthorDto> CreateAsync(AuthorCreateDto input)
        {

            var author = await _authorManager.CreateAsync(
            input.Name, input.Birthdate, input.Active
            );

            return ObjectMapper.Map<Author, AuthorDto>(author);
        }

        [Authorize(CRM_CodePermissions.Authors.Edit)]
        public virtual async Task<AuthorDto> UpdateAsync(Guid id, AuthorUpdateDto input)
        {

            var author = await _authorManager.UpdateAsync(
            id,
            input.Name, input.Birthdate, input.Active, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Author, AuthorDto>(author);
        }
    }
}