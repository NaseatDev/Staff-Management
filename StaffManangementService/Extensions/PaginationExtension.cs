using Microsoft.EntityFrameworkCore;
using StaffManagement.Service.Commons.ResponseResult;

namespace StaffManagement.Service.Extensions
{
    public static class PaginationExtension
    {
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken) where T : class
        {
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var count = await source.CountAsync(cancellationToken: cancellationToken);
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            return PaginatedResult<T>.Create(items, count, pageNumber, pageSize);
        }
    }
}
