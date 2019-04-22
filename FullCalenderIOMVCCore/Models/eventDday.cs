using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderIOMVCCore.Models
{
    public class eventDday
    {
        public int Id { get; set; }
        public string EventDay { get; set; }
        public DateTime Dday { get; set; }
        public IList<DdayDetails> DdayDetails { get; set; }
    }
    public class DdayDetails
    {
        public int Id { get; set; }
        public int eventDdayId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string Color { get; set; }
        public bool isFullDay { get; set; }
    }
}
