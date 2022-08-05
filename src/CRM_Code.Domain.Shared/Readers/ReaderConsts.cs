namespace CRM_Code.Readers
{
    public static class ReaderConsts
    {
        private const string DefaultSorting = "{0}NameSurname asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Reader." : string.Empty);
        }

        public const int NameSurnameMinLength = 0;
        public const int NameSurnameMaxLength = 9999;
    }
}