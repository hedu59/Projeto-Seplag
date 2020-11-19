using Prototype.Domain.Interfaces.IUnitOfWork.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Domain.Interfaces.IUnitOfWork.Extensions
{
    public static class PagedListExtensions
    {
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, int indexFrom = 0)
        {
            return new PagedList<T>(source: source, pageIndex: pageIndex, pageSize: pageSize, indexFrom: indexFrom);
        }
    }
}
