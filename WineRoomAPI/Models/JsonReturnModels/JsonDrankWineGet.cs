using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineRoomAPI.Models.JsonReturnModels
{
    public class JsonDrankWineGet
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<DrankWine> Data { get; set; }
    }
}