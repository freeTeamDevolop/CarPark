using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebFramework.Infrastructure.Ulitily.Performance.ObjectCounters;

namespace WebFramework.Data.Infrastructure
{
    public abstract class DbContextLocator: ITransactionManager, IDisposable
    {
        private static int IdSeed = 0;
        private DbContext _context;
        public int LocatorId { get; private set; }

        public DbContext GetDbContext(string identity)
        {
            Demand(identity);
            return _context;
        }

        public void Demand(string identity)
        {
            EnsureContext(identity);
        }

        public void RequireNew(string identity)
        {
            DisposeContext();
            EnsureContext(identity);
        }
        public async Task SaveAsync()
        {
            Exception exeption = null;
            if (_context != null)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbEntityValidationException e)
                {
                    var errorMsg = new StringBuilder();
                    foreach (var errors in e.EntityValidationErrors)
                    {
                        foreach (var error in errors.ValidationErrors)
                        {
                            errorMsg.AppendFormat("Entity:{0},property:{1},error:{2}",
                                errors.Entry.Entity.GetType().Name
                                , error.PropertyName, error.ErrorMessage);
                        }
                    }
                    exeption = new Exception(errorMsg.ToString(), e);
                }
                catch (Exception e)
                {
                    exeption = e;
                }
                finally
                {
                    ObjectCounter.DecreseDbContextCount(LocatorId);
                    _context.Dispose();
                    _context = null;
                }
                if (exeption != null)
                {
                    throw exeption;
                }
            }
        }
        private void DisposeContext()
        {
            Exception exeption = null;
            if (_context != null)
            {
                try
                {
                    var cancel = false;
                    if (HttpContext.Current != null)
                    {
                        if (HttpContext.Current.Items["tran_cancel"] != null)
                        {
                            if (HttpContext.Current.Items["tran_cancel"].ToString() == "1")
                                cancel = true;
                        }
                    }
                    if (!cancel)
                        _context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    var errorMsg = new StringBuilder();
                    foreach (var errors in e.EntityValidationErrors)
                    {
                        foreach (var error in errors.ValidationErrors)
                        {
                            errorMsg.AppendFormat("Entity:{0},property:{1},error:{2}",
                                errors.Entry.Entity.GetType().Name
                                , error.PropertyName, error.ErrorMessage);
                        }
                    }
                    exeption = new Exception(errorMsg.ToString(), e);
                }
                catch (Exception e)
                {
                    exeption = e;
                }
                finally
                {
                    ObjectCounter.DecreseDbContextCount(LocatorId);
                    _context.Dispose();
                    _context = null;
                }
                if (exeption != null)
                {
                    throw exeption;
                }
            }
        }

        private void EnsureContext(string identity)
        {
            if (_context != null)
            {
                return;
            }
            _context = GetContext();
            Interlocked.Increment(ref IdSeed);
            LocatorId = IdSeed;
            ObjectCounter.IncreseDbContextCount(LocatorId,
                string.Format("source:{0},str:{1}", identity, _context.Database.Connection.ConnectionString));
        }

        [SuppressMessage("ReSharper", "RedundantLambdaSignatureParentheses")]
        private DbContext GetContext()
        {
            var context = BuildContext();
            context.Configuration.ProxyCreationEnabled = false;
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.UseDatabaseNullSemantics = false;
            //context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.ValidateOnSaveEnabled = false;
            context.Database.CommandTimeout = 240;
            //context.Database.CompatibleWithModel
            // ReSharper disable once RedundantLambdaSignatureParentheses
            context.Database.Log = (s) =>
            {
                //if (s.Contains("sql_mode"))
                //    Debug.Write(s);
            };
            return context;
        }

        protected abstract DbContext BuildContext();

        public void Dispose()
        {
            DisposeContext();
        }


    }

    public sealed class FrameworkContextLocator : DbContextLocator //
    {
        protected override DbContext BuildContext()
        {
            var connectionString = ConnectionString.GetConnectionString("webframework");
            return new FrameworkContext(connectionString);
        }
    }
}

