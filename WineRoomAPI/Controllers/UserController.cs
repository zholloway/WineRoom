﻿using Newtonsoft.Json;
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
            return Ok(userServices.JsonUserGet(userServices.GetUser()));
        }

        [HttpPost]
        public IHttpActionResult CheckUser()
        {
            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;
            User user = JsonConvert.DeserializeObject<User>(jsonContent);

            if (userServices.CheckForUser(user) == true)
            {
                return Ok(userServices.JsonUserReturn(userServices.FindUser(user).ID));
            }

            return Ok(userServices.JsonUserReturn(-1));
        }

        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            userServices.DeleteUser(id);
            return Ok(userServices.JsonUserReturn(id));
        }
    }
}
