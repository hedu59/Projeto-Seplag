using Flunt.Notifications;
using MediatR;
using Prototype.Application.Commands.Input.Servidores;
using Prototype.Application.Interfaces.Filas;
using Prototype.Domain.Commands.Output;
using Prototype.Domain.Entities;
using Prototype.Domain.Enums;
using Prototype.Domain.Interfaces.IUnitOfWork;
using Prototype.Domain.Interfaces.Repositories;
using Prototype.Shared.Auth;
using Prototype.Shared.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Prototype.Application.Handlers
{
    public class ServidorHandler : Notifiable,
        IRequestHandler<CreateServidorCommand, ICommandResult>,
        IRequestHandler<UpdateServidorCommand, ICommandResult>,
        IRequestHandler<DeleteServidorCommand, ICommandResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUser _user;
        private readonly ITransacaoMongoRepository _mongoRepository;
        private readonly ILogTransacaoMensagem _logTransacao;
        public ServidorHandler(IUnitOfWork uow, IUser user, ITransacaoMongoRepository mongoRepository, ILogTransacaoMensagem logTransacao)
        {
            _uow = uow;
            _user = user;
            _mongoRepository = mongoRepository;
            _logTransacao = logTransacao;
        }

        public async Task<ICommandResult> Handle(CreateServidorCommand command, CancellationToken cancellationToken)
        {
            try
            {

                command.Validate();

                if (!command.Valid)
                    return new CommandResult(success: false, message: "Erro ao criar servidor", data: command.Notifications);

                var beneficio = new Servidor(nome: command.Nome, cpf: command.CPF, orgao: command.Orgao, matricula: command.Matricula, ESetoresTramitacao.Setorial_Servidor);

                _uow.GetRepository<Servidor>()
                .Save(beneficio);
                GravarTramitacao(beneficio, ESetoresTramitacao.Setorial_Servidor, ESetoresTramitacao.Setorial_Servidor);

                _uow.SaveChanges();

                await CriarLog(beneficio.Id, "Servidor Criado");

                return await Task.FromResult(new CommandResult(success: true, message: "Servidor salvo com sucesso", data: beneficio));
            }
            catch (Exception ex)
            {
                new CommandResult(success: false, message: ex.Message, data: null);
                return await Task.FromResult(new CommandResult(success: false, message: "Erro ao salvar Servidor.", data: command.Notifications));
            }
        }

        private async Task CriarLog(Guid servidorId, string obsevacao)
        {
            var log = new LogTransacao() { Observacao = obsevacao, ServidorId = servidorId };
            _logTransacao.ProduzirTransacao(log);
          
        }

        public async Task<ICommandResult> Handle(UpdateServidorCommand command, CancellationToken cancellationToken)
        {
            try
            {

                command.Validate();

                if (!command.Valid)
                    return new CommandResult(success: false, message: null, data: command.Notifications);


                var servidor = _uow
                .GetRepository<Servidor>()
                    .GetFirstOrDefault(predicate: x => x.Id == command.ServidorId);

                if (servidor != null)
                {
                    var setorAnterior = servidor.SetorTramitacao;

                    servidor.UpdateServidor(command.Tramitacao);
                    _uow.GetRepository<Servidor>().Update(entity: servidor);

                    GravarTramitacao(servidor, setorAnterior, command.Tramitacao);

                    _uow.SaveChanges();

                    await CriarLog(servidor.Id, "Servidor Alterado");

                    return await Task.FromResult(new CommandResult(success: true, message: "Servidor alterado com sucesso", data: command));
                }

                return await Task.FromResult(new CommandResult(success: false, message: "Servidor nao encontrado", data: command));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new CommandResult(success: false, message: ex.Message, data: null));
            }
        }
        public async Task<ICommandResult> Handle(DeleteServidorCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var servidor = _uow.GetRepository<User>().GetFirstOrDefault(predicate: x => x.Id == command.ServidorId);

                servidor.Disable();

                _uow.GetRepository<User>().Update(entity: servidor);

                _uow.SaveChanges();

                await CriarLog(servidor.Id, "Servidor Deletado");

                return await Task.FromResult(new CommandResult(success: true, message: "Usuário removido com sucesso", data: null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new CommandResult(success: false, message: ex.Message, data: null));
            }
        }

        private void GravarTramitacao(Servidor beneficio, ESetoresTramitacao setorAnterior, ESetoresTramitacao tramitacao)
        {
            try
            {

                var documentoTramitacao = new ProcessoTramitacao(

               servidorId: beneficio.Id,
               data: DateTime.Now,
               origem: setorAnterior,
               destino: tramitacao,
               usuario: _user.Name == null ? "Carlos Eduardo" : _user.Name);
                { }


                _uow.GetRepository<ProcessoTramitacao>()
                    .Save(entity: documentoTramitacao);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

    }
}
