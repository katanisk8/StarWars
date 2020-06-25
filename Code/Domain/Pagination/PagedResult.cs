using System.Collections.Generic;

namespace CodePepper.Domain.Pagination
{
    public class PagedResult<T>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalRows { get; set; }

        public List<T> Data { get; set; }
    }
}
