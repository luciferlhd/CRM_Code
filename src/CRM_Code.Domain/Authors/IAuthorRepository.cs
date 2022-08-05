using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace CRM_Code.Authors
{
    public interface IAuthorRepository : IRepository<Author, Guid>
    {
        Task<List<Author>> GetListAsync(
            string filterText = null,
            string name = null,
            DateTime? birthdateMin = null,
            DateTime? birthdateMax = null,
            bool? active = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string name = null,
            DateTime? birthdateMin = null,
            DateTime? birthdateMax = null,
            bool? active = null,
            CancellationToken cancellationToken = default);
    }
}