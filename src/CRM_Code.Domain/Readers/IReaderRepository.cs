using CRM_Code.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace CRM_Code.Readers
{
    public interface IReaderRepository : IRepository<Reader, Guid>
    {
        Task<ReaderWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<ReaderWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string nameSurname = null,
            string emailAddress = null,
            Gender? gender = null,
            Guid? bookId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Reader>> GetListAsync(
                    string filterText = null,
                    string nameSurname = null,
                    string emailAddress = null,
                    Gender? gender = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string nameSurname = null,
            string emailAddress = null,
            Gender? gender = null,
            Guid? bookId = null,
            CancellationToken cancellationToken = default);
    }
}