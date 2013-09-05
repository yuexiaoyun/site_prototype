using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.Scheduler.Action.Log
{
    public class ActionLog
    {
        public int Id { get; set; }
        public string ActionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string LogPath { get; set; }
    }
}
