using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MockedContext
{
    public class InjectableMockedContext<TContext> where TContext : DbContext
    {
        private int _changeCount = 0;
        private Mock<TContext> _mockConext;

        public InjectableMockedContext()
        {
            _mockConext = new Mock<TContext>();
            _mockConext.Setup(x => x.SaveChanges()).Returns(() =>
            {
                var copy = _changeCount;
                _changeCount = 0;
                return copy;
            });

            _mockConext.Setup(x => x.SaveChangesAsync()).Returns(() =>
            {
                var copy = _changeCount;
                _changeCount = 0;
                return Task.FromResult(copy);
            });
        }

        public Mock<TContext> MockedContext { get { return _mockConext; } }

        public Mock<DbSet<TEntity>> MockEntity<TEntity>(Expression<Func<TContext, DbSet<TEntity>>> dbSetToMock,
            List<TEntity> seed = null) where TEntity : class
        {
            Mock<DbSet<TEntity>> _innerMock = new Mock<DbSet<TEntity>>();
            if (seed == null)
                seed = new List<TEntity>();

            var queryableSeed = seed.AsQueryable();

            _innerMock.As<IDbAsyncEnumerable<TEntity>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<TEntity>(queryableSeed.GetEnumerator()));

            _innerMock.As<IQueryable<TEntity>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<TEntity>(queryableSeed.Provider));

            _innerMock.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(queryableSeed.Expression);
            _innerMock.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(queryableSeed.ElementType);
            _innerMock.As<IQueryable<TEntity>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => queryableSeed.GetEnumerator());

            _innerMock.Setup(x => x.Add(It.IsAny<TEntity>())).Returns<TEntity>(e =>
            {
                _changeCount++;
                seed.Add(e);
                queryableSeed = seed.AsQueryable();
                return e;
            });
            _innerMock.Setup(x => x.AddRange(It.IsAny<IEnumerable<TEntity>>())).Returns<IEnumerable<TEntity>>(e =>
            {
                _changeCount += e.Count();
                seed.AddRange(e);
                queryableSeed = seed.AsQueryable();
                return e;
            });

            _mockConext.Setup(dbSetToMock).Returns(_innerMock.Object);
            return _innerMock;
        }
    }
}