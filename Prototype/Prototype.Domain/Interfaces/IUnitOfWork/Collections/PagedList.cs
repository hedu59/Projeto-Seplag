using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype.Domain.Interfaces.IUnitOfWork.Collections
{
    public class PagedList<T> : IPagedList<T>
    {
        public int PageSize { get; private set; }

        public int IndexFrom { get; private set; }

        public int PageIndex { get; private set; }

        public int TotalCount { get; private set; }

        public int TotalPages { get; private set; }

        public bool HasNextPage => PageIndex - IndexFrom + 1 < TotalPages;

        public bool HasPreviousPage => PageIndex - IndexFrom > 0;

        public IList<T> Items { get; private set; }

        internal PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int indexFrom)
        {
            if (indexFrom > pageIndex)
                throw new ArgumentException($"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");

            PageIndex = pageIndex;
            PageSize = pageSize;
            IndexFrom = indexFrom;

            if (source is IQueryable<T> queryable)
            {

                TotalCount = queryable.Count();
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

                Items = queryable.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToList();

            }
            else
            {
                TotalCount = source.Count();
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

                Items = source.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToList();
            }
        }

        internal PagedList() => Items = new T[0];
    }
}
