using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace CRM_Code.Authors
{
    public class AuthorUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(AuthorConsts.NameMaxLength, MinimumLength = AuthorConsts.NameMinLength)]
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public bool Active { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}