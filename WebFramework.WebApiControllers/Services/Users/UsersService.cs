using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Data.Infrastructure;
using WebFramework.Data.Models;

namespace WebFramework.WebApiControllers.Services.Users
{
    public class UsersService:IUsersService
    {


        private readonly IRepository<Data.Models.Users> userRepo;

        public UsersService(IRepository<Data.Models.Users> userRepo)
        {
            this.userRepo = userRepo;
        }

        public bool isValidUser()
        {
            return true;
        }

        public List<UserResult> GetUserInfo()
        {
            var result = new List<UserResult>();

            var usersList = from u in userRepo.Table
                            select new UserResult
                            {
                                id = u.id,
                                userName = u.userName
                            };
            result = usersList.ToList();
            return result;

        }
    }

    public class UserResult
    {
        public string userName { get; set; }

        public long id { get; set; }
    }

}
