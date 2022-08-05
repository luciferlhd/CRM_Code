using CRM_Code.Books;
using CRM_Code.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace CRM_Code.Readers
{
    public class ReaderManager : DomainService
    {
        private readonly IReaderRepository _readerRepository;
        private readonly IRepository<Book, Guid> _bookRepository;

        public ReaderManager(IReaderRepository readerRepository,
        IRepository<Book, Guid> bookRepository)
        {
            _readerRepository = readerRepository;
            _bookRepository = bookRepository;
        }

        public async Task<Reader> CreateAsync(
        List<Guid> bookIds,
        string nameSurname, string emailAddress, Gender gender)
        {
            var reader = new Reader(
             GuidGenerator.Create(),
             nameSurname, emailAddress, gender
             );

            await SetBooksAsync(reader, bookIds);

            return await _readerRepository.InsertAsync(reader);
        }

        public async Task<Reader> UpdateAsync(
            Guid id,
            List<Guid> bookIds,
        string nameSurname, string emailAddress, Gender gender, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _readerRepository.WithDetailsAsync(x => x.Books);
            var query = queryable.Where(x => x.Id == id);

            var reader = await AsyncExecuter.FirstOrDefaultAsync(query);

            reader.NameSurname = nameSurname;
            reader.EmailAddress = emailAddress;
            reader.Gender = gender;

            await SetBooksAsync(reader, bookIds);

            reader.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _readerRepository.UpdateAsync(reader);
        }

        private async Task SetBooksAsync(Reader reader, List<Guid> bookIds)
        {
            if (bookIds == null || !bookIds.Any())
            {
                reader.RemoveAllBooks();
                return;
            }

            var query = (await _bookRepository.GetQueryableAsync())
                .Where(x => bookIds.Contains(x.Id))
                .Select(x => x.Id);

            var bookIdsInDb = await AsyncExecuter.ToListAsync(query);
            if (!bookIdsInDb.Any())
            {
                return;
            }

            reader.RemoveAllBooksExceptGivenIds(bookIdsInDb);

            foreach (var bookId in bookIdsInDb)
            {
                reader.AddBook(bookId);
            }
        }

    }
}