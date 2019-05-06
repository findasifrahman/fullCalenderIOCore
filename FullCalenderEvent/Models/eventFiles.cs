using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullCalenderEvent.Models
{
    public class eventFiles
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string fileFolder { get; set; }
    }
}