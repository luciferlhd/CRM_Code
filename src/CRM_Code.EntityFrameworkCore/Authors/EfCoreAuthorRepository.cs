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

namespace CRM_Code.Authors
{
    public class EfCoreAuthorRepository : EfCoreRepository<CRM_CodeDbContext, Author, Guid>, IAuthorRepository
    {
        public EfCoreAuthorRepository(IDbContextProvider<CRM_CodeDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Author>> GetListAsync(
            string filterText = null,
            string name = null,
            DateTime? birthdateMin = null,
            DateTime? birthdateMax = null,
            bool? active = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, birthdateMin, birthdateMax, active);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AuthorConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string name = null,
            DateTime? birthdateMin = null,
            DateTime? birthdateMax = null,
            bool? active = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, birthdateMin, birthdateMax, active);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Author> ApplyFilter(
            IQueryable<Author> query,
            string filterText,
            string name = null,
            DateTime? birthdateMin = null,
            DateTime? birthdateMax = null,
            bool? active = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(birthdateMin.HasValue, e => e.Birthdate >= birthdateMin.Value)
                    .WhereIf(birthdateMax.HasValue, e => e.Birthdate <= birthdateMax.Value)
                    .WhereIf(active.HasValue, e => e.Active == active);
        }
    }
}