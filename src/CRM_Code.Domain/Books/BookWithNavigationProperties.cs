using CRM_Code.Authors;

using System;
using System.Collections.Generic;

namespace CRM_Code.Books
{
    public class BookWithNavigationProperties
    {
        public Book Book { get; set; }

        public Author Author { get; set; }
        

        
    }
}