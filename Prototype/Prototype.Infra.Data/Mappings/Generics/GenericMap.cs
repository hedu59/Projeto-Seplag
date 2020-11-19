using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prototype.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Infra.Data.Mappings.Generics
{
    public class GenericMap<T> where T : Entity
    {
        public void DefaultMap(EntityTypeBuilder<T> builder, string tableName)
        {
            builder.ToTable(tableName);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Active);
            builder.Property(x => x.RegisterDate)
                .IsRequired();
        }
    }
}
