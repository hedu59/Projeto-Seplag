using Prototype.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Application.Interfaces.Filas
{
    public  interface ILogTransacaoMensagem
    {
        void ProduzirTransacao(LogTransacao message);
    }
}
