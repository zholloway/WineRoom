using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineRoomAPI.Models
{
    public class JsonWineGet
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Wine> Data { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}