using CRM_Code.Enums;
using CRM_Code.Books;
using CRM_Code.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using CRM_Code.EntityFrameworkCore;

namespace CRM_Code.Readers
{
    public class EfCoreReaderRepository : EfCoreRepository<CRM_CodeDbContext, Reader, Guid>, IReaderRepository
    {
        public EfCoreReaderRepository(IDbContextProvider<CRM_CodeDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<ReaderWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id).Include(x => x.Books)
                .Select(reader => new ReaderWithNavigationProperties
                {
                    Reader = reader,
                    Books = (from readerBooks in reader.Books
                             join _book in dbContext.Set<Book>() on readerBooks.BookId equals _book.Id
                             select _book).ToList()
                }).FirstOrDefault();
        }

        public async Task<List<ReaderWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string nameSurname = null,
            string emailAddress = null,
            Gender? gender = null,
            Guid? bookId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, nameSurname, emailAddress, gender, bookId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ReaderConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ReaderWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from reader in (await GetDbSetAsync())

                   select new ReaderWithNavigationProperties
                   {
                       Reader = reader,
                       Books = new List<Book>()
                   };
        }

        protected virtual IQueryable<ReaderWithNavigationProperties> ApplyFilter(
            IQueryable<ReaderWithNavigationProperties> query,
            string filterText,
            string nameSurname = null,
            string emailAddress = null,
            Gender? gender = null,
            Guid? bookId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Reader.NameSurname.Contains(filterText) || e.Reader.EmailAddress.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(nameSurname), e => e.Reader.NameSurname.Contains(nameSurname))
                    .WhereIf(!string.IsNullOrWhiteSpace(emailAddress), e => e.Reader.EmailAddress.Contains(emailAddress))
                    .WhereIf(gender.HasValue, e => e.Reader.Gender == gender)
                    .WhereIf(bookId != null && bookId != Guid.Empty, e => e.Reader.Books.Any(x => x.BookId == bookId));
        }

        public async Task<List<Reader>> GetListAsync(
            string filterText = null,
            string nameSurname = null,
            string emailAddress = null,
            Gender? gender = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, nameSurname, emailAddress, gender);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ReaderConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string nameSurname = null,
            string emailAddress = null,
            Gender? gender = null,
            Guid? bookId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, nameSurname, emailAddress, gender, bookId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Reader> ApplyFilter(
            IQueryable<Reader> query,
            string filterText,
            string nameSurname = null,
            string emailAddress = null,
            Gender? gender = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.NameSurname.Contains(filterText) || e.EmailAddress.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(nameSurname), e => e.NameSurname.Contains(nameSurname))
                    .WhereIf(!string.IsNullOrWhiteSpace(emailAddress), e => e.EmailAddress.Contains(emailAddress))
                    .WhereIf(gender.HasValue, e => e.Gender == gender);
        }
    }
}