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
    public class BeneficioServidorMap : GenericMap<BeneficioServidor>, IEntityTypeConfiguration<BeneficioServidor>, IEntityMapping
    {
        public void Configure(EntityTypeBuilder<BeneficioServidor> builder)
        {
            DefaultMap(builder: builder, tableName: "Servidores");

            builder.Property(x => x.CPF)
                .HasColumnName("CPF")
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(x => x.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(80)
                .IsRequired();


            builder.Property(x => x.Matricula)
                .HasMaxLength(40)
                .HasColumnName("Matricula")
                .IsRequired();

            builder.Property(x => x.Orgao)
                .HasColumnName("Orgao")
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(x => x.SetorDescricao)
                .HasColumnName("SetorDescricao");

            builder.Property(x => x.SetorTramitacao)
             .HasColumnName("Setor_Atual")
             .IsRequired();

            builder.HasMany(x => x.Documentos)
                .WithOne()
                .HasForeignKey(x => x.ServidorId);

            builder.HasMany(x => x.Tramitacoes)
                .WithOne()
                .HasForeignKey(x=> x.ServidorId);

        }
    }
}
