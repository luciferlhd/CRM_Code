using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace CRM_Code.Authors
{
    public class Author : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }

        public virtual DateTime Birthdate { get; set; }

        public virtual bool Active { get; set; }

        public Author()
        {

        }

        public Author(Guid id, string name, DateTime birthdate, bool active)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), AuthorConsts.NameMaxLength, AuthorConsts.NameMinLength);
            Name = name;
            Birthdate = birthdate;
            Active = active;
        }

    }
}