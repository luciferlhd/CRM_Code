using Volo.Abp.Application.Dtos;
using System;

namespace CRM_Code.Books
{
    public class GetBooksInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Title { get; set; }
        public int? PageCountMin { get; set; }
        public int? PageCountMax { get; set; }
        public float? PirceMin { get; set; }
        public float? PirceMax { get; set; }
        public Guid? AuthorId { get; set; }

        public GetBooksInput()
        {

        }
    }
}