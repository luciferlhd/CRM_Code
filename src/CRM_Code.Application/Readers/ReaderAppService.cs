using CRM_Code.Shared;
using CRM_Code.Books;
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
using CRM_Code.Readers;

namespace CRM_Code.Readers
{

    [Authorize(CRM_CodePermissions.Readers.Default)]
    public class ReadersAppService : ApplicationService, IReadersAppService
    {
        private readonly IReaderRepository _readerRepository;
        private readonly ReaderManager _readerManager;
        private readonly IRepository<Book, Guid> _bookRepository;

        public ReadersAppService(IReaderRepository readerRepository, ReaderManager readerManager, IRepository<Book, Guid> bookRepository)
        {
            _readerRepository = readerRepository;
            _readerManager = readerManager; _bookRepository = bookRepository;
        }

        public virtual async Task<PagedResultDto<ReaderWithNavigationPropertiesDto>> GetListAsync(GetReadersInput input)
        {
            var totalCount = await _readerRepository.GetCountAsync(input.FilterText, input.NameSurname, input.EmailAddress, input.Gender, input.BookId);
            var items = await _readerRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.NameSurname, input.EmailAddress, input.Gender, input.BookId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ReaderWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ReaderWithNavigationProperties>, List<ReaderWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ReaderWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<ReaderWithNavigationProperties, ReaderWithNavigationPropertiesDto>
                (await _readerRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<ReaderDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Reader, ReaderDto>(await _readerRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetBookLookupAsync(LookupRequestDto input)
        {
            var query = (await _bookRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Title != null &&
                         x.Title.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Book>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Book>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(CRM_CodePermissions.Readers.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _readerRepository.DeleteAsync(id);
        }

        [Authorize(CRM_CodePermissions.Readers.Create)]
        public virtual async Task<ReaderDto> CreateAsync(ReaderCreateDto input)
        {

            var reader = await _readerManager.CreateAsync(
            input.BookIds, input.NameSurname, input.EmailAddress, input.Gender
            );

            return ObjectMapper.Map<Reader, ReaderDto>(reader);
        }

        [Authorize(CRM_CodePermissions.Readers.Edit)]
        public virtual async Task<ReaderDto> UpdateAsync(Guid id, ReaderUpdateDto input)
        {

            var reader = await _readerManager.UpdateAsync(
            id,
            input.BookIds, input.NameSurname, input.EmailAddress, input.Gender, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Reader, ReaderDto>(reader);
        }
    }
}