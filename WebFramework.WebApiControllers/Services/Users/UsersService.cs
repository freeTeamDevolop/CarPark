using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.WebApiControllers.Services.Users
{
    public class UsersService:IUsersService
    {
        public UsersService()
        {

        }

        public bool isValidUser()
        {
            return true;
        }
    }
}
