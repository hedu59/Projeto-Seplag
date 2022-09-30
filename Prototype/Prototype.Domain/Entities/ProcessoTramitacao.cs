using Prototype.Domain.Enums;
using Prototype.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Domain.Entities
{
    public class ProcessoTramitacao : Entity
    {
        public Guid ServidorId { get; private set; }
        public Servidor Documento { get; private set; }
        public DateTime DataTramitacao { get; private set; }
        public ESetoresTramitacao SetorOrigem { get; private set; }
        public ESetoresTramitacao SetorDestino { get; private set; }
        public virtual string SetorOrigemDescricao { get;  set; }
        public virtual string SetorDestinoDescricao { get;  set; }
        public string UsuarioMovimentacao { get; private set; }

        public ProcessoTramitacao(Guid servidorId, DateTime data, ESetoresTramitacao origem, ESetoresTramitacao destino, string usuario)
        {
            ServidorId = servidorId;
            DataTramitacao = data;
            SetorOrigem = origem;
            SetorDestino = destino;
            UsuarioMovimentacao = usuario;    
        }

        public ProcessoTramitacao()
        {

        }

    }
}
