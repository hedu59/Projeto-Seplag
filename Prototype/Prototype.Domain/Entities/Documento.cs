using Prototype.Domain.Enums;
using Prototype.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Domain.Entities
{
    public class Documento : Entity
    {
        public Documento()
        {

        }
        public Documento(Guid id, string name, string size, string type, DateTime date, string base64, byte[] fileByte, ECategoriaDocumento categoria)
        {
            ServidorId = id;
            FileName = name;
            FileSize = size;
            FileType = type;
            LastModifiedDate = date;
            FileAsBase64 = base64;
            FileAsByteArray = fileByte;
            Categoria = categoria;
        }


        public Guid ServidorId { get; private set; }
        public Servidor Servidor { get; private set; }
        public string FileName { get; private set; }
        public string FileSize { get; private set; }
        public string FileType { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
        public string FileAsBase64 { get; private set; }
        public byte[] FileAsByteArray { get; private set; }
        public ECategoriaDocumento Categoria { get; private set; }     
        public virtual string CategoriaDescicao { get;  set; }


        public void UpdateDocumento( DateTime date)
        {
            LastModifiedDate = date;
        }
    }

}
