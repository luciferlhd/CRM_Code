namespace CRM_Code.Authors
{
    public static class AuthorConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Author." : string.Empty);
        }

        public const int NameMinLength = 1;
        public const int NameMaxLength = 100;
    }
}