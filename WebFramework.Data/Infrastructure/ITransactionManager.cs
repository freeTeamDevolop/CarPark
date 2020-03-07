using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WebFramework.Data.Infrastructure
{
    public interface ITransactionManager
    {
        int LocatorId { get; }
        void Demand(string identity);

        void RequireNew(string identity);

        //void Cancel();
        DbContext GetDbContext(string identity);
        Task SaveAsync();
    }
}
