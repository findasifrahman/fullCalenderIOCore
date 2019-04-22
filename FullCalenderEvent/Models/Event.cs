using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FullCalenderEvent.Models
{
    public class Event
    {
        [key]
        public int EventID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Start { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? End { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }
    }
}