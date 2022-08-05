using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace CRM_Code.Books
{
    public class BookManager : DomainService
    {
        private readonly IBookRepository _bookRepository;

        public BookManager(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        // hàm này để tạo
        public async Task<Book> CreateAsync(
        Guid? authorId, string title, int pageCount, float pirce)
        {
            var book = new Book(
             GuidGenerator.Create(),
             authorId, title, pageCount, pirce
             );

            return await _bookRepository.InsertAsync(book);
        }
        //hàm update
        public async Task<Book> UpdateAsync(
            Guid id,
            Guid? authorId, string title, int pageCount, float pirce, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _bookRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);
            // hàm thực thi
            var book = await AsyncExecuter.FirstOrDefaultAsync(query);

            book.AuthorId = authorId;
            book.Title = title;
            book.PageCount = pageCount;
            book.Pirce = pirce;
            //Đặt tem đồng thời nếu không có giá trị 
            book.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _bookRepository.UpdateAsync(book);
        }

    }
}