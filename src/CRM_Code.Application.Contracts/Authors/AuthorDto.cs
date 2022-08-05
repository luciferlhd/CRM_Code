using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace CRM_Code.Authors
{
    public class AuthorDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public bool Active { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}