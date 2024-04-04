using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskServiceAPI.DAL_Lyr
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string RequestMethod_ { get; set; }
        public string RequestUri_ { get; set; }
        public Nullable<System.DateTime> TimeUtc_ { get; set; }
    }
}