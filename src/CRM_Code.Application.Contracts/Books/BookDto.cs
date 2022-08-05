using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace CRM_Code.Books
{
    public class BookDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public float Pirce { get; set; }
        public Guid? AuthorId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}