using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Data.Infrastructure;

namespace WebFramework.Framework.Services.Common
{
    public abstract class DbServiceBase<TModel> : ServiceBase
        where TModel : class
    {
        private readonly IRepository<TModel> _repo;

        protected IRepository<TModel> Repo
        {
            get { return _repo; }
        }

        protected DbServiceBase(
            IRepository<TModel> repo)
        {
            _repo = repo;
        }
    }
}
