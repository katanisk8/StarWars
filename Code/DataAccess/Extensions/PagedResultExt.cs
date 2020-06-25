using CodePepper.Domain.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CodePepper.DataAccess.Extensions
{
    public static class PagedResultExt
    {
        public static async Task<PagedResult<TResult>> GetPagedResultAsync<TResult>(this IQueryable<TResult> query, Paging paging) where TResult : class
        {
            int count = await query.CountAsync().ConfigureAwait(false);

            if (paging.PageSize > 0)
            {
                int offset = (paging.Page - 1) * paging.PageSize;
                query = query.Skip(offset).Take(paging.PageSize);
            }

            return new PagedResult<TResult>
            {
                Page = paging.Page,
                PageSize = paging.PageSize,
                TotalRows = count,
                Data = query.ToList()
            };
        }
    }
}
