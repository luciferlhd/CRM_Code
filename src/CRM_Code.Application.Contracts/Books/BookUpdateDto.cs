using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace CRM_Code.Books
{
    public class BookUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(BookConsts.TitleMaxLength, MinimumLength = BookConsts.TitleMinLength)]
        public string Title { get; set; }
        [Required]
        [Range(BookConsts.PageCountMinLength, BookConsts.PageCountMaxLength)]
        public int PageCount { get; set; }
        public float Pirce { get; set; }
        public Guid? AuthorId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}