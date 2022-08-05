using CRM_Code.Authors;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace CRM_Code.Books
{
    public class Book : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Title { get; set; }

        public virtual int PageCount { get; set; }

        public virtual float Pirce { get; set; }
        public Guid? AuthorId { get; set; }

        public Book()
        {

        }

        public Book(Guid id, Guid? authorId, string title, int pageCount, float pirce)
        {

            Id = id;
            Check.NotNull(title, nameof(title));
            Check.Length(title, nameof(title), BookConsts.TitleMaxLength, BookConsts.TitleMinLength);
            if (pageCount < BookConsts.PageCountMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(pageCount), pageCount, "The value of 'pageCount' cannot be lower than " + BookConsts.PageCountMinLength);
            }

            if (pageCount > BookConsts.PageCountMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(pageCount), pageCount, "The value of 'pageCount' cannot be greater than " + BookConsts.PageCountMaxLength);
            }

            Title = title;
            PageCount = pageCount;
            Pirce = pirce;
            AuthorId = authorId;
        }

    }
}