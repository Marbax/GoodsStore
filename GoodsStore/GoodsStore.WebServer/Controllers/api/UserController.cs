using GoodsStore.Business.Models;
using GoodsStore.Business.Models.Concrete;
using GoodsStore.Business.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace GoodsStore.WebServer.Controllers.api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private readonly IServicesUnitOfWork _uow;

        public UserController(IServicesUnitOfWork uow)
        {
            _uow = uow;
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Http.Route("login")]
        public dynamic Login(UserDTO user)
        {
            if (!ModelState.IsValid)
                return new { Code = "401", Message = "Invalid model" };

            //var user = _uow.Users.Get(i => i.Name == user.Name && i.Password == user.Password).FirstOrDefault();

            return new { Name = "User", Token = "someusertoken" };
        }

    }
}
