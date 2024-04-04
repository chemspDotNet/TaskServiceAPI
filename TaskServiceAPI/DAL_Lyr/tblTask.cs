using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace TaskServiceAPI.DAL_Lyr
{
    public class tblTask
    {
        [Key]
        public int QuoteNumber { get; set; }
        public string QuoteType { get; set; }
        public string Contact { get; set; }
        public string TaskDescription { get; set; }
        public string TaskType { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; } = default(Nullable<System.DateTime>);
    }
}