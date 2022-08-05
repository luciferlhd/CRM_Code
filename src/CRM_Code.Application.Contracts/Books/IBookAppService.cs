using CRM_Code.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CRM_Code.Books
{
    public interface IBooksAppService : IApplicationService
    {
        Task<PagedResultDto<BookWithNavigationPropertiesDto>> GetListAsync(GetBooksInput input);
        //Lấy  Với dẫn đường Đặc tínhKhông đồng bộ
        Task<BookWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<BookDto> GetAsync(Guid id);
        //tra cứu
        Task<PagedResultDto<LookupDto<Guid?>>> GetAuthorLookupAsync(LookupRequestDto input);
        // xóa
        Task DeleteAsync(Guid id);
        // tạo
        Task<BookDto> CreateAsync(BookCreateDto input);
        // update
        Task<BookDto> UpdateAsync(Guid id, BookUpdateDto input);
    }
}