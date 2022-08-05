using CRM_Code.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CRM_Code.Readers
{
    public interface IReadersAppService : IApplicationService
    {
        Task<PagedResultDto<ReaderWithNavigationPropertiesDto>> GetListAsync(GetReadersInput input);

        Task<ReaderWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<ReaderDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetBookLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<ReaderDto> CreateAsync(ReaderCreateDto input);

        Task<ReaderDto> UpdateAsync(Guid id, ReaderUpdateDto input);
    }
}