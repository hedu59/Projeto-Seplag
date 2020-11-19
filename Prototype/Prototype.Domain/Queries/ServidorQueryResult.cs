using Prototype.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Domain.Queries
{
    public class ServidorQueryResult
    {
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Orgao { get; private set; }
        public string Matricula { get; private set; }
        public IEnumerable<Documento> Documentos { get; set; }
    }
}
