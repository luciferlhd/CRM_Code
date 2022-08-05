using CRM_Code.Enums;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace CRM_Code.Readers
{
    public class ReaderDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string NameSurname { get; set; }
        public string EmailAddress { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public Gender Gender { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}