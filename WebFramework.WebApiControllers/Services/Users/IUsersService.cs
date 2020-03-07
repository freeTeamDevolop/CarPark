using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.WebApiControllers.Services.Users
{
    public interface IUsersService
    {
        bool isValidUser();

        List<UserResult> GetUserInfo();
    }
}
