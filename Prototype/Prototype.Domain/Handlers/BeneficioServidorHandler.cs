using Flunt.Notifications;
using Prototype.Domain.Commands.Input.Servidores;
using Prototype.Domain.Commands.Output;
using Prototype.Domain.Entities;
using Prototype.Domain.Enums;
using Prototype.Domain.Interfaces.IUnitOfWork;
using Prototype.Shared.Auth;
using Prototype.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Domain.Handlers
{
    public class BeneficioServidorHandler : Notifiable,
        ICommandHandler<CreateBeneficioServidorCommand>,
        ICommandHandler<UpdateBeneficioServidorCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUser _user;

        public BeneficioServidorHandler(IUnitOfWork uow, IUser user)
        {
            _uow = uow;
            _user = user;
        }

        public ICommandResult Handle(CreateBeneficioServidorCommand command)
        {
            try
            {
                var beneficio = new BeneficioServidor(nome: command.Nome, cpf: command.CPF, orgao: command.Orgao, matricula: command.Matricula, ESetoresTramitacao.Setorial_Servidor);
                _uow.GetRepository<BeneficioServidor>()
                .Save(beneficio);
                _uow.SaveChanges();

                GravarTramitacao(beneficio, ESetoresTramitacao.Setorial_Servidor, ESetoresTramitacao.Setorial_Servidor);

                return new CommandResult(success: true, message: "Servidor salvo com sucesso", data: command.Notifications);
            }
            catch (Exception ex)
            {
                return new CommandResult(success: false, message: ex.Message, data: null);
            }
        }

        public ICommandResult Handle(UpdateBeneficioServidorCommand command)
        {
            try
            {
                var servidor = _uow
                    .GetRepository<BeneficioServidor>()
                    .GetFirstOrDefault(predicate: x => x.Id == command.ServidorId);

                if (servidor != null)
                {
                    var setorAnterior = servidor.SetorTramitacao;
                    
                    servidor.UpdateServidor(command.Tramitacao);
                    _uow.GetRepository<BeneficioServidor>().Update(entity: servidor);
                    _uow.SaveChanges();

                    GravarTramitacao(servidor, setorAnterior, command.Tramitacao);

                    return new CommandResult(success: true, message: "Servidor alterado com sucesso", data: command);
                }

                return new CommandResult(success: false, message: "Servidor nao encontrada", data: command);
            }
            catch (Exception ex)
            {
                return new CommandResult(success: false, message: ex.Message, data: null);
            }
        }

        public ICommandResult Handle(Guid id)
        {
            try
            {
                var user = _uow.GetRepository<User>().GetFirstOrDefault(predicate: x => x.Id == id);

                user.Disable();

                _uow.GetRepository<User>().Update(entity: user);

                _uow.SaveChanges();

                return new CommandResult(success: true, message: "Usuário removido com sucesso", data: null);
            }
            catch (Exception ex)
            {
                return new CommandResult(success: false, message: ex.Message, data: null);
            }
        }

        private void GravarTramitacao(BeneficioServidor beneficio, ESetoresTramitacao setorAnterior, ESetoresTramitacao tramitacao)
        {
            try
            {

                var documentoTramitacao = new ProcessoTramitacao(

               servidorId: beneficio.Id,
               data: DateTime.Now,
               origem: setorAnterior,
               destino: tramitacao,
               usuario: _user.Name == null ? "Carlos Eduardo" : _user.Name);{ }


                _uow.GetRepository<ProcessoTramitacao>()
                    .Save(entity: documentoTramitacao);

                _uow.SaveChanges();
            }
            catch (Exception ex)
            {

            }

        }
    }
}
