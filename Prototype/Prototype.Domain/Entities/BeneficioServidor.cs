using Prototype.Domain.Enums;
using Prototype.Shared.Entities;
using System.Collections.Generic;

namespace Prototype.Domain.Entities
{
    public class BeneficioServidor : Entity
    {
        public BeneficioServidor()
        {

        }
        public BeneficioServidor(string nome, string cpf, string orgao, string matricula, ESetoresTramitacao setor)
        {
            Nome = nome;
            CPF = cpf;
            Orgao = orgao;
            Matricula = matricula;
            SetorTramitacao = setor;
        }

        public string Nome { get;private set; }
        public string CPF { get; private set; }
        public string Orgao { get; private set; }
        public string Matricula { get; private set; }
        public ESetoresTramitacao SetorTramitacao { get;  set; }
        public virtual string SetorDescricao { get; set; }
        public IEnumerable<Documento> Documentos { get; set; }
        public IEnumerable<ProcessoTramitacao> Tramitacoes { get; set; }


        public void UpdateServidor(ESetoresTramitacao tramitacao)
        {

            SetorTramitacao = tramitacao;

        }  
    }
}
