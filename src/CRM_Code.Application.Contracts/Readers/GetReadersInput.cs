using CRM_Code.Enums;
using Volo.Abp.Application.Dtos;
using System;

namespace CRM_Code.Readers
{
    public class GetReadersInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string NameSurname { get; set; }
        public string EmailAddress { get; set; }
        public Gender? Gender { get; set; }
        public Guid? BookId { get; set; }

        public GetReadersInput()
        {

        }
    }
}