﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineRoomAPI.Models
{
    public class PutDrankWine
    {
        public int ID { get; set; }
        public DateTime DateDrank { get; set; }
        public string People { get; set; }
        public string Location { get; set; }
        public string Comments { get; set; }
        public Wine Wine { get; set; }
    }
}