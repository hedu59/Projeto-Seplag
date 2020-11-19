using Microsoft.EntityFrameworkCore;
using Prototype.Application.Interfaces;
using Prototype.Domain.Commands.Input.Documentos;
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
    public class DocumentoService : IDocumentoService
    {
        private readonly DocumentoHandler _handler;
        private readonly IUnitOfWork _uow;

        public DocumentoService(DocumentoHandler handler, IUnitOfWork uow)
        {
            _handler = handler;
            _uow = uow;
        }

        public ICommandResult CreateDocumento(CreateDocumentoCommand command)
        {
            command.Validate();

            if (!command.Valid)
                return new CommandResult(success: false, message: null, data: command.Notifications);

            return _handler.Handle(command);

        }

        public ICommandResult UpdateDocumento(UpdateDocumentoCommand command)
        {
            command.Validate();

            if (!command.Valid)
                return new CommandResult(success: false, message: null, data: command.Notifications);

            return _handler.Handle(command);
        }

        public ICommandResult DeleteDocumento(Guid id)
        {
            return _handler.Handle(id);
        }

        public IPagedList<Documento> ObterListDeDocumento(int pageIndex, int pageSize)
        {
            try
            {
                var documentos = _uow.GetRepository<Documento>()
                                     .GetPagedList(predicate: x => x.Active == true, 
                                     disableTracking: true, 
                                     pageIndex: pageIndex, 
                                     pageSize: pageSize);

                documentos = ConverterCategoria(documentos);

                return documentos;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IPagedList<Documento> ObterListDeDocumentoPorServidor(Guid ServidorId, int pageIndex, int pageSize)
        {
            try
            {
                var documentos = _uow.GetRepository<Documento>().GetPagedList(
                   predicate: x => x.ServidorId == ServidorId &&
                   x.Active == true,
                   disableTracking: true,
                   pageIndex: pageIndex, pageSize: pageSize);

                documentos = ConverterCategoria(documentos);

                return documentos;
            }
            catch (Exception ex)
            {
                throw;
            }
           

        }

        public Documento ObterDocumentoPorID(Guid Id)
        {
            var documento = _uow.GetRepository<Documento>().GetFirstOrDefault(
                 predicate: x => x.Id == Id && x.Active,
                 disableTracking: true);

            return documento;
        }

        #region MetodosAuxiliares
    
       
        private IPagedList<Documento> ConverterCategoria(IPagedList<Documento> documentos)
        {

            foreach (var item in documentos.Items)
            {
                switch (item.Categoria)
                {
                    case ECategoriaDocumento.Identificacao:
                        item.CategoriaDescicao = "Identificacao";
                        break;
                    case ECategoriaDocumento.VidaFuncional:
                        item.CategoriaDescicao = "Vida Funcional";
                        break;
                    case ECategoriaDocumento.ContagemDeTempo:
                        item.CategoriaDescicao = "Contagem De Tempo";
                        break;
                    case ECategoriaDocumento.Remuneracao_Proventos:
                        item.CategoriaDescicao = "Remuneracao de Proventos";
                        break;
                    default:
                        break;

                }

            }
            return documentos;
        }

        #endregion

    }
}
