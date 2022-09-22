using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ImageGallery.Data
{
    internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            throw new NotImplementedException();
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
