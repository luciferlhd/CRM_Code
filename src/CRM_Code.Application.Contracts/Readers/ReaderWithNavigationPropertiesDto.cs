using CRM_Code.Books;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace CRM_Code.Readers
{
    public class ReaderWithNavigationPropertiesDto
    {
        public ReaderDto Reader { get; set; }

        public List<BookDto> Books { get; set; }

    }
}