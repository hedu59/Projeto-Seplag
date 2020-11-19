using Microsoft.EntityFrameworkCore;
using Prototype.Application.Interfaces;
using Prototype.Domain.Commands.Input.Servidores;
using Prototype.Domain.Commands.Output;
using Prototype.Domain.Entities;
using Prototype.Domain.Enums;
using Prototype.Domain.Handlers;
using Prototype.Domain.Interfaces.IUnitOfWork;
using Prototype.Domain.Interfaces.IUnitOfWork.Collections;
using Prototype.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype.Application.Services
{
    public class ServidorService : IServidorService
    {

        private readonly BeneficioServidorHandler _handler;
        private readonly IUnitOfWork _uow;

        public ServidorService(BeneficioServidorHandler handler, IUnitOfWork uow)
        {
            _handler = handler;
            _uow = uow;
        }

        public ICommandResult CreateServidor(CreateBeneficioServidorCommand command)
        {
            command.Validate();

            if (!command.Valid)
                return new CommandResult(success: false, message: "Erro ao criar servidor", data: command.Notifications);

            return _handler.Handle(command);

        }

        public ICommandResult UpdateServidor(UpdateBeneficioServidorCommand command)
        {
            command.Validate();

            if (!command.Valid)
                return new CommandResult(success: false, message: null, data: command.Notifications);

            return _handler.Handle(command);
        }

        public ICommandResult DeleteServidor(Guid id)
        {
            return _handler.Handle(id);
        }

        public BeneficioServidor ObterTramitacoesPorID(Guid Id)
        {
            var servidor = _uow.GetRepository<BeneficioServidor>().GetFirstOrDefault(
               predicate: x => x.Id == Id && x.Active,
               disableTracking: true);

            var tramitacao = _uow.GetRepository<ProcessoTramitacao>()
                                .Get(predicate: x => x.ServidorId == Id && x.Active, disableTracking: true).ToList();

            tramitacao = ConverterSetores(tramitacao);
            servidor.SetorDescricao = tramitacao.LastOrDefault().SetorDestinoDescricao;
            servidor.SetorTramitacao = tramitacao.LastOrDefault().SetorDestino;

            servidor.Tramitacoes = tramitacao;

            return servidor;
        }

        private List<ProcessoTramitacao> ConverterSetores(List<ProcessoTramitacao> tramitacao)
        {
            foreach (var item in tramitacao)
            {
                switch (item.SetorOrigem)
                {
                    case ESetoresTramitacao.Setorial_Servidor:
                        item.SetorOrigemDescricao = "Setorial Servidor";
                        break;
                    case ESetoresTramitacao.CPrev_Gestor:
                        item.SetorOrigemDescricao = "CPrev Gestor";
                        break;
                    case ESetoresTramitacao.Secretario_SEPLAG:
                        item.SetorOrigemDescricao = "Secretario SEPLAG";
                        break;
                    case ESetoresTramitacao.PGE_Analise:
                        item.SetorOrigemDescricao = "PGE Analise";
                        break;
                    case ESetoresTramitacao.TCE_Gestor:
                        item.SetorOrigemDescricao = "TCE Gestor";
                        break;
                    default:
                        break;
                }

                switch (item.SetorDestino)
                {
                    case ESetoresTramitacao.Setorial_Servidor:
                        item.SetorDestinoDescricao = "Setorial Servidor";
                        break;
                    case ESetoresTramitacao.CPrev_Gestor:
                        item.SetorDestinoDescricao = "CPrev Gestor";
                        break;
                    case ESetoresTramitacao.Secretario_SEPLAG:
                        item.SetorDestinoDescricao = "Secretario SEPLAG";
                        break;
                    case ESetoresTramitacao.PGE_Analise:
                        item.SetorDestinoDescricao = "PGE Analise";
                        break;
                    case ESetoresTramitacao.TCE_Gestor:
                        item.SetorDestinoDescricao = "TCE Gestor";
                        break;
                    default:
                        break;
                }
            }

            return tramitacao;
        }
        private IPagedList<BeneficioServidor> ConverterSetores(IPagedList<BeneficioServidor> beneficio)
        {

            foreach (var item in beneficio.Items)
            {
                switch (item.SetorTramitacao)
                {
                    case Domain.Enums.ESetoresTramitacao.Setorial_Servidor:
                        item.SetorDescricao = "Setorial Servidor";
                        break;
                    case Domain.Enums.ESetoresTramitacao.CPrev_Gestor:
                        item.SetorDescricao = "CPrev Gestor";
                        break;
                    case Domain.Enums.ESetoresTramitacao.Secretario_SEPLAG:
                        item.SetorDescricao = "Secretario SEPLAG";
                        break;
                    case Domain.Enums.ESetoresTramitacao.PGE_Analise:
                        item.SetorDescricao = "PGE Analise";
                        break;
                    case Domain.Enums.ESetoresTramitacao.TCE_Gestor:
                        item.SetorDescricao = "TCE Gestor";
                        break;
                    default:
                        break;
                }

            }

            return beneficio;
        }


    }
}
