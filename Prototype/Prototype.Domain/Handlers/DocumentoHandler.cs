using Flunt.Notifications;
using Prototype.Domain.Commands.Input.Documentos;
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
    public class DocumentoHandler : Notifiable,
        ICommandHandler<CreateDocumentoCommand>,
        ICommandHandler<UpdateDocumentoCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUser _user;

        public DocumentoHandler(IUnitOfWork uow, IUser user)
        {
            _uow = uow;
            _user = user;
        }

        public ICommandResult Handle(CreateDocumentoCommand command)
        {
            try
            {
              var servidor = _uow
                   .GetRepository<BeneficioServidor>()
                   .GetFirstOrDefault(predicate: x => x.Id == command.ServidorId);

                if(servidor != null)
                {
                    var documento = new Documento(
                        id: command.ServidorId,
                        name: command.FileName,
                        size: command.FileSize,
                        type: command.FileType,
                        date: DateTime.Now,
                        base64: command.FileAsBase64,
                        fileByte: command.FileAsByteArray,
                        categoria: command.Categoria)
                    { };

                    _uow.GetRepository<Documento>().Save(documento);
                    _uow.SaveChanges();

                    return new CommandResult(success: true, message: "Arquivo salvo com sucesso", data: command);
                }

                return new CommandResult(success: false, message: "Servidor nao encontrado", data: command);

            }
            catch (Exception ex)
            {
                return new CommandResult(success: false, message: ex.Message, data: null);
            }
        }


        public ICommandResult Handle(UpdateDocumentoCommand command)
        {
            try
            {
                var servidor = _uow
                    .GetRepository<BeneficioServidor>()
                    .GetFirstOrDefault(predicate: x => x.Id == command.ServidorId);

                if (servidor != null)
                {

                    var documento = _uow
                        .GetRepository<Documento>()
                        .GetFirstOrDefault(predicate: x => x.Id == command.DocumentoId);

                    documento.UpdateDocumento(DateTime.Now);

                    _uow.GetRepository<Documento>().Update(entity: documento);
                    _uow.SaveChanges();


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
    }
}
