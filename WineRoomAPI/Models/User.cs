using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineRoomAPI.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool adminStatus { get; set; }

        public virtual ICollection<Wine> Wines { get; set; }
    }
}