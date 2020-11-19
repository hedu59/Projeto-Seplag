using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Shared
{
    public interface IResponseDefault
    {
        bool Success { get; set; }
        string Message { get; set; }
        object Data { get; set; }
    }
}
