﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineRoomAPI.Models.JsonReturnModels
{
    public class JsonDrankWineCrud
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int DrankWineID { get; set; }
    }
}