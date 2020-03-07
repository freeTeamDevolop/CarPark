﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebFramework.Framework.Api.Controllers;
using WebFramework.Framework.Data;
using WebFramework.WebApiControllers.Services.Users;

namespace WebFramework.WebApiControllers.Controllers
{
    public class UsersController:ApiCoreController
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public RequestResult<bool> isValidUser()
        {
            return F(() => _usersService.isValidUser());
        }

        [HttpGet]
        public RequestResult<List<UserResult>> GetUserInfo()
        {
            return F(() => _usersService.GetUserInfo());
        }
    }
}
