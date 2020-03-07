using Autofac;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Z.EntityFramework.Plus;

namespace WebFramework.Data.Infrastructure
{
    public sealed class Repository<T>: IRepository<T>
        where T : class
    {
        private readonly ITransactionManager _contextLocator;

        //private readonly DbSet<T> _dbSet;
        public Repository(IComponentContext componentContext)
        {
            var locatorName = "FrameworkContextLocator";
            
            _contextLocator = componentContext.ResolveKeyed<ITransactionManager>(locatorName);
        }

        private DbContext DbContext
        {
            get { return _contextLocator.GetDbContext(string.Format("{0}:GetNew", typeof(T).Name)); }
        }

        public IQueryable<T> Table
        {
            get { return DbContext.Set<T>(); }
        }

        public IQueryable DynamicTable(Type type)
        {
            return DbContext.Set(type);
        }
        private ObjectContext ObjectContext
        {
            get { return (DbContext as IObjectContextAdapter).ObjectContext; }
        }

        public int LocatorId => _contextLocator.LocatorId;

        

        public void AddRange(IEnumerable<T> entities)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _contextLocator.RequireNew("");
                DbContext.Configuration.AutoDetectChangesEnabled = false;
                int count = 0;
                foreach (var entityToInsert in entities)
                {
                    ++count;
                    AddToContext(entityToInsert, count, 100);
                }
                Flush();
                scope.Complete();
            }
        }
        private void AddToContext(T entity, int count, int commitCount)
        {
            Insert(entity);
            if (count % commitCount == 0)
            {
                _contextLocator.RequireNew("");
                DbContext.Configuration.AutoDetectChangesEnabled = false;
            }
        }
        public int Update(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updateFactory)
        {
            return DbContext.Set<T>().Where(predicate).Update(updateFactory);
        }
        public int DeleteBatch(Expression<Func<T, bool>> predicate, Action<BatchDelete> batchDeleteBuilder = null)
        {
            var query = DbContext.Set<T>().Where(predicate);
            if (batchDeleteBuilder != null)
                return query.Delete(batchDeleteBuilder);
            else
                return query.Delete();
        }
        public T Insert(T entity)
        {
            return DbContext.Set<T>().Add(entity);
        }
        public void Delete(T entityToDelete)
        {
            if (DbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbContext.Set<T>().Attach(entityToDelete);
            }
            DbContext.Set<T>().Remove(entityToDelete);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return Table.Where(predicate).FirstOrDefault();
        }

        public void Flush()
        {
            var o = _contextLocator as IDisposable;
            if (o != null) o.Dispose();
        }
        public async Task FlushAsync()
        {
            var o = _contextLocator as ITransactionManager;
            if (o != null) await o.SaveAsync();
        }
        public void Save()
        {
            if (_contextLocator != null) _contextLocator.RequireNew(string.Format("{0}:RequireNew", typeof(T).Name));
        }

        public List<TModel> ExecuteQuery<TModel>(string sql, params object[] parameters)
        {
            return DbContext.Database.SqlQuery<TModel>(sql, parameters).ToList();
        }
        public int ExecuteCommand(string sql, params object[] parameters)
        {
            return DbContext.Database.ExecuteSqlCommand(sql, parameters);
        }

        public void AddRangeBuildIn(IEnumerable<T> entities)
        {
            DbContext.Set<T>().AddRange(entities);
        }

        public void SetReadUncommited()
        {
            DbContext.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
        }

        public DbContextTransaction CreateDBTransaction()
        {
            return DbContext.Database.BeginTransaction();
        }

    }
}
