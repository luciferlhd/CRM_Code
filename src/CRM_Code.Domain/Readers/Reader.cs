using CRM_Code.Enums;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace CRM_Code.Readers
{
    public class Reader : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string NameSurname { get; set; }

        [CanBeNull]
        public virtual string EmailAddress { get; set; }

        public virtual Gender Gender { get; set; }

        public ICollection<ReaderBook> Books { get; private set; }

        public Reader()
        {

        }

        public Reader(Guid id, string nameSurname, string emailAddress, Gender gender)
        {

            Id = id;
            Check.NotNull(nameSurname, nameof(nameSurname));
            Check.Length(nameSurname, nameof(nameSurname), ReaderConsts.NameSurnameMaxLength, ReaderConsts.NameSurnameMinLength);
            NameSurname = nameSurname;
            EmailAddress = emailAddress;
            Gender = gender;
            Books = new Collection<ReaderBook>();
        }
        public void AddBook(Guid bookId)
        {
            Check.NotNull(bookId, nameof(bookId));

            if (IsInBooks(bookId))
            {
                return;
            }

            Books.Add(new ReaderBook(Id, bookId));
        }

        public void RemoveBook(Guid bookId)
        {
            Check.NotNull(bookId, nameof(bookId));

            if (!IsInBooks(bookId))
            {
                return;
            }

            Books.RemoveAll(x => x.BookId == bookId);
        }

        public void RemoveAllBooksExceptGivenIds(List<Guid> bookIds)
        {
            Check.NotNullOrEmpty(bookIds, nameof(bookIds));

            Books.RemoveAll(x => !bookIds.Contains(x.BookId));
        }

        public void RemoveAllBooks()
        {
            Books.RemoveAll(x => x.ReaderId == Id);
        }

        private bool IsInBooks(Guid bookId)
        {
            return Books.Any(x => x.BookId == bookId);
        }
    }
}