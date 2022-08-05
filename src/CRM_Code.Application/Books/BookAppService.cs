using CRM_Code.Shared;
using CRM_Code.Authors;
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
using CRM_Code.Books;

namespace CRM_Code.Books
{

    [Authorize(CRM_CodePermissions.Books.Default)]
    public class BooksAppService : ApplicationService, IBooksAppService
    {
        private readonly IBookRepository _bookRepository;
        private readonly BookManager _bookManager;
        private readonly IRepository<Author, Guid> _authorRepository;

        public BooksAppService(IBookRepository bookRepository, BookManager bookManager, IRepository<Author, Guid> authorRepository)
        {
            _bookRepository = bookRepository;
            _bookManager = bookManager; _authorRepository = authorRepository;
        }
        // lấy danh sách và kêt hợp  với lọc khi tìm kiếm  , tổng bản ghi truyền vào  GetCountAsync 
        // lọc qua GetListWithNavigationPropertiesAsync
        public virtual async Task<PagedResultDto<BookWithNavigationPropertiesDto>> GetListAsync(GetBooksInput input)
        {
            var totalCount = await _bookRepository.GetCountAsync(input.FilterText, input.Title, input.PageCountMin, input.PageCountMax, input.PirceMin, input.PirceMax, input.AuthorId);
            var items = await _bookRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Title, input.PageCountMin, input.PageCountMax, input.PirceMin, input.PirceMax, input.AuthorId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<BookWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                //Đặt chỗ với thuộc tính điều hướng map với DTO
                Items = ObjectMapper.Map<List<BookWithNavigationProperties>, List<BookWithNavigationPropertiesDto>>(items)
            };
        }
        //Nhận với thuộc tính điều hướng ; phục  vụ cho chức năng edit hàm onget Edit.Cshtm
        public virtual async Task<BookWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<BookWithNavigationProperties, BookWithNavigationPropertiesDto>
                (await _bookRepository.GetWithNavigationPropertiesAsync(id));
        }
        //  api này get by id
        public virtual async Task<BookDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Book, BookDto>(await _bookRepository.GetAsync(id));
        }

        //phục vụ cho cả edit , creat,edit nhé,
        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetAuthorLookupAsync(LookupRequestDto input)
        {
            //tra cứu
            var query = (await _authorRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            //tra cứu dữ liệu sau khi tra cứu quẻy 
            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Author>();
            // lấy ra tổng bản ghi
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Author>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        // phân quyền cho nút xóa
        [Authorize(CRM_CodePermissions.Books.Delete)]

        public virtual async Task DeleteAsync(Guid id)
        {
            await _bookRepository.DeleteAsync(id);
        }
        // phân quyenf tạo
        [Authorize(CRM_CodePermissions.Books.Create)]
        // api create
        public virtual async Task<BookDto> CreateAsync(BookCreateDto input)
        {

            var book = await _bookManager.CreateAsync(
            input.AuthorId, input.Title, input.PageCount, input.Pirce
            );

            return ObjectMapper.Map<Book, BookDto>(book);
        }

        [Authorize(CRM_CodePermissions.Books.Edit)]
        public virtual async Task<BookDto> UpdateAsync(Guid id, BookUpdateDto input)
        {

            var book = await _bookManager.UpdateAsync(
            id,
            input.AuthorId, input.Title, input.PageCount, input.Pirce, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Book, BookDto>(book);
        }
    }
}