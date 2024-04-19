using System;
using System.Collections.Generic;

namespace Project.Core.AppPagedList
{
    public class PagedList<T>
    {
        public PagedList(List<T> items, int count, int pageNumber, int itemsCountPerPage)
        {
            TotalCount = count;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)itemsCountPerPage);
            Items = items;
        }
        public List<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }   
}
