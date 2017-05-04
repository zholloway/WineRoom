using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineRoomAPI.Models
{
    public class JSONreturn
    {
        public string success { get; set; }
        public string message { get; set; }
        public List<Wine> data { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}