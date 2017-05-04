using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WineRoomAPI.Models;
using WineRoomAPI.Services;
using WineRoomAPI.Services.DrankWineServices;

namespace WineRoomAPI.Controllers
{
    [EnableCors(origins: "http://wineroomonline.gear.host", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        UserServices userServices = new UserServices();

        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(userServices.JsonUserGet(userServices.GetUsers()));
        }

        [HttpPost]
        public IHttpActionResult EditDrankWine(int id, User user)
        {
            userServices.EditUser(id, user);
            return Ok(userServices.JsonUserReturn(id));
        }

        [HttpPost]
        public IHttpActionResult AddUser(User user)
        {
            userServices.AddUser(user);
            var newUser = userServices.FindNewUser(user);
            return Ok(userServices.JsonUserReturn(newUser.ID));
        }

        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            userServices.DeleteUser(id);
            return Ok(userServices.JsonUserReturn(id));
        }
    }
}
