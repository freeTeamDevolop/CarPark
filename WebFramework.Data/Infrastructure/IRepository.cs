using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace WebFramework.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        int LocatorId { get; }
        T Insert(T entity);
        void AddRange(IEnumerable<T> entities);
        void AddRangeBuildIn(IEnumerable<T> entities);
        void Delete(T entity);
        int DeleteBatch(Expression<Func<T, bool>> predicate, Action<BatchDelete> batchDeleteBuilder = null);
        T Get(Expression<Func<T, bool>> predicate);
        IQueryable<T> Table { get; }
        IQueryable DynamicTable(Type type);
        void Flush();
        Task FlushAsync();
        int Update(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updateFactory);
        void SetReadUncommited();

        List<TModel> ExecuteQuery<TModel>(string sql, params object[] parameters);
        int ExecuteCommand(string sql, params object[] parameters);

        DbContextTransaction CreateDBTransaction();
    }
}
