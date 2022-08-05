using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CRM_Code.Authors
{
    public class AuthorCreateDto
    {
        [Required]
        [StringLength(AuthorConsts.NameMaxLength, MinimumLength = AuthorConsts.NameMinLength)]
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public bool Active { get; set; }
    }
}