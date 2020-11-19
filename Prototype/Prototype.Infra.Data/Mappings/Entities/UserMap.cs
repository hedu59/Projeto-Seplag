using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prototype.Domain.Entities;
using Prototype.Infra.Data.Interfaces;
using Prototype.Infra.Data.Mappings.Generics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Infra.Data.Mappings.Entities
{
    public class UserMap : GenericMap<User>, IEntityTypeConfiguration<User>, IEntityMapping 
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            DefaultMap(builder: builder, tableName: "Users");

            builder
                .Property(x => x.Login)
                .HasColumnName("Login")
                .HasDefaultValue("Admin")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.Password)
                .HasColumnName("Password")
                .HasDefaultValue("123456")
                .HasMaxLength(15)
                .IsRequired();

            builder
                .Property(x => x.Email)
                .HasColumnName("Email")
                .HasDefaultValue("admin@prototype.com")
                .HasMaxLength(100)
                .IsRequired();

        }
    }
}
