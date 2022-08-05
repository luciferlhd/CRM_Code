using Volo.Abp.Application.Dtos;
using System;

namespace CRM_Code.Authors
{
    public class GetAuthorsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Name { get; set; }
        public DateTime? BirthdateMin { get; set; }
        public DateTime? BirthdateMax { get; set; }
        public bool? Active { get; set; }

        public GetAuthorsInput()
        {

        }
    }
}