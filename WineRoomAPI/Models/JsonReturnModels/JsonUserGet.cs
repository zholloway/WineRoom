using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineRoomAPI.Models.JsonReturnModels
{
    public class JsonUserGet
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<User> Data { get; set; }
    }
}