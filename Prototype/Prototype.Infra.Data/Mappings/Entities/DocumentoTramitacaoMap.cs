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
    //class DocumentoTramitacaoMap
    //{
    //}

    public class DocumentoTramitacaoMap : GenericMap<ProcessoTramitacao>, IEntityTypeConfiguration<ProcessoTramitacao>, IEntityMapping
    {
        public void Configure(EntityTypeBuilder<ProcessoTramitacao> builder)
        {
            DefaultMap(builder: builder, tableName: "Tramitacao");

            builder.Property(x => x.DataTramitacao)
                .HasColumnName("Data_Tramitacao")
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

            builder.Property(x => x.SetorOrigem)
                .HasColumnName("Setor_Origem")
                .IsRequired();

            builder.Property(x => x.SetorDestino)
                .HasColumnName("Setor_Destino")
                .IsRequired();

            builder.Property(x => x.SetorDestinoDescricao)
                .HasColumnName("Setor_Destino_Descricao");

            builder.Property(x => x.SetorOrigemDescricao)
                .HasColumnName("Setor_Origem_Descricao");

            builder.Property(x => x.UsuarioMovimentacao)
                .HasMaxLength(50)
                .HasColumnName("Usuario")
                .IsRequired();

        }
    }
}
