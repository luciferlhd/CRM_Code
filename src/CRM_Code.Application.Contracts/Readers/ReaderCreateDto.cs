using CRM_Code.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CRM_Code.Readers
{
    public class ReaderCreateDto
    {
        [Required]
        [StringLength(ReaderConsts.NameSurnameMaxLength, MinimumLength = ReaderConsts.NameSurnameMinLength)]
        public string NameSurname { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public Gender Gender { get; set; } = ((Gender[])Enum.GetValues(typeof(Gender)))[0];
        public List<Guid> BookIds { get; set; }
    }
}