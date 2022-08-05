namespace CRM_Code.Books
{
    public static class BookConsts
    {
        private const string DefaultSorting = "{0}Title asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Book." : string.Empty);
        }

        public const int TitleMinLength = 5;
        public const int TitleMaxLength = 500;
        public const int PageCountMinLength = 10;
        public const int PageCountMaxLength = 99999;
    }
}