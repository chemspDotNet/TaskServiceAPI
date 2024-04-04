using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TaskServiceAPI.DAL_Lyr;

namespace TaskServiceAPI.Controllers
{
    public class HomeController : Controller
    {
        static TaskDBContext taskEntities = new TaskDBContext();
        public ActionResult Index()
        {
            tblTask tsk = new tblTask();
            tsk.Contact = "Sunil Patel";
            tsk.QuoteType = "DYR";
            tsk.TaskDescription = "This is Task One Description";
            tsk.TaskType = "Follow=up";

            DAL_Lyr.tblTask task = taskEntities.tblTasks.ToList().Where(x => x.QuoteNumber == 1).FirstOrDefault();

            if(task == null )
            {
                var user = taskEntities.tblTasks.Add(tsk);
                taskEntities.SaveChanges();

            }

            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
