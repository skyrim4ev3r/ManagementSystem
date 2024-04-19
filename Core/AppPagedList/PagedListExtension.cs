using Microsoft.EntityFrameworkCore;
using Project.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Core.AppPagedList
{
    public static class PagedListExtension
    {
        public static async Task<PagedList<T>> CreatePagedListAsync<T>(this IQueryable<T> source, PagedListDto dto)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((dto.PageNumber - 1) * dto.ItemsCountPerPage).Take(dto.ItemsCountPerPage).ToListAsync();
            return new PagedList<T>(items, count, dto.PageNumber, dto.ItemsCountPerPage);
        }       
    }
}