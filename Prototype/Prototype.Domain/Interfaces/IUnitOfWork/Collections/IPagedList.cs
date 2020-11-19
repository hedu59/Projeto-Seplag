using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Domain.Interfaces.IUnitOfWork.Collections
{
    public interface IPagedList<T>
    {
        int PageSize { get; }
        int IndexFrom { get; }
        int PageIndex { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasNextPage { get; }
        bool HasPreviousPage { get; }

        IList<T> Items { get; }
    }
}
