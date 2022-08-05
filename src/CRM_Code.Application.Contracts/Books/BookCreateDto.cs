using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CRM_Code.Books
{
    public class BookCreateDto
    {
        [Required]
        [StringLength(BookConsts.TitleMaxLength, MinimumLength = BookConsts.TitleMinLength)]
        public string Title { get; set; }
        [Required]
        [Range(BookConsts.PageCountMinLength, BookConsts.PageCountMaxLength)]
        public int PageCount { get; set; }
        public float Pirce { get; set; }
        public Guid? AuthorId { get; set; }
    }
}