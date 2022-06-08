using MediatR;

namespace Omikron.SharedKernel.Api.Models
{
    public abstract class PaginationQuery<TEntity> : IRequest<TEntity>
    {
        private int _page;
        private int _pageSize;

        public int PageSize
        {
            get => _pageSize == default ? 10 : _pageSize;
            set => _pageSize = value;
        }

        public int Page
        {
            get => _page == default ? 1 : _page;
            set => _page = value;
        }
    }
}