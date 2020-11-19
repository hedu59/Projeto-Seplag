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
    public class DocumentoMap : GenericMap<Documento>, IEntityTypeConfiguration<Documento>, IEntityMapping
    {
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
            DefaultMap(builder: builder, tableName: "Documentos");

            builder.Property(x => x.FileAsBase64)
                .HasColumnName("Arquivo_Base64")
                .IsRequired();

            builder.Property(x => x.FileAsByteArray)
                .HasColumnName("Bytes")
                .IsRequired();


            builder.Property(x => x.FileName)
                .HasMaxLength(200)
                .HasColumnName("Nome_Arquivo")
                .IsRequired();

            builder.Property(x => x.LastModifiedDate)
                .HasColumnName("Ultima_Modificacao")
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

            builder.Property(x => x.FileSize)
                    .HasColumnName("Tamanho_Arquivo")
                    .IsRequired();

            builder.Property(x => x.FileType)
                .HasColumnName("Tipo")
                .HasDefaultValue("application/pdf")
                .IsRequired();

            builder.Property(x => x.Categoria)
             .HasColumnName("Categoria")
             .IsRequired();

            builder.Property(x => x.CategoriaDescicao)
                .HasColumnName("CategoriaDescicao");

            builder.HasOne(x => x.Servidor)
                .WithMany()
                .HasForeignKey(x => x.ServidorId);

        }
    }
}
