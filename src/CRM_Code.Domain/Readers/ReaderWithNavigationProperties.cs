using CRM_Code.Books;

using System;
using System.Collections.Generic;

namespace CRM_Code.Readers
{
    public class ReaderWithNavigationProperties
    {
        public Reader Reader { get; set; }

        

        public List<Book> Books { get; set; }
        
    }
}