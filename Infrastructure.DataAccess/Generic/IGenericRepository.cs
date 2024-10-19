using System.Linq.Expressions;

namespace Infrastructure.DataAccess.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        IEnumerable<T> FindByAsync(Expression<Func<T, bool>> WhereCondition);

        IEnumerable<T> ExecuteStoredProcedure<U>(string sp_name, IEnumerable<(string, object)> parameters);

        IEnumerable<T> ExecuteQuery<U>(string query, IEnumerable<(string, object)> parameters);

        IUnitOfWork UnitOfWork { get; }
    }
}