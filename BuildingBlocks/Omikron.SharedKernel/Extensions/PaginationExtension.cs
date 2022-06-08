using System.Linq;
using Omikron.SharedKernel.Api.Models;

namespace Omikron.SharedKernel.Extensions
{
    public static class PaginationExtension
    {
        public static IQueryable<TEntity> WithPagination<TEntity, TResponse>(this IQueryable<TEntity> query, PaginationQuery<TResponse> paginationQuery)
        {
            return query
                .Skip(paginationQuery.PageSize * (paginationQuery.Page - 1))
                .Take(paginationQuery.PageSize);
        }
    }
}
