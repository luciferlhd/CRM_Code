using System;
using Volo.Abp.Domain.Entities;

namespace CRM_Code.Readers
{
    public class ReaderBook : Entity
    {

        public Guid ReaderId { get; protected set; }

        public Guid BookId { get; protected set; }

        private ReaderBook()
        {

        }

        public ReaderBook(Guid readerId, Guid bookId)
        {
            ReaderId = readerId;
            BookId = bookId;
        }

        public override object[] GetKeys()
        {
            return new object[]
                {
                    ReaderId,
                    BookId
                };
        }
    }
}