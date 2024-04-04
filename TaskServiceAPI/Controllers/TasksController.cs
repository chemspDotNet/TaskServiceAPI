using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskServiceAPI.AuthenticationService;
using TaskServiceAPI.DAL_Layer;
using TaskServiceAPI.CustomFilters;
using System.Web.Http.Cors;

namespace TaskServiceAPI.Controllers
{
   // [CustomExceptionFilter]
   // [JWTAuthenticationFilter]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TasksController : ApiController
    {
        static TaskDBEntities1 taskEntities = new TaskDBEntities1();

        public IHttpActionResult GetTasks()
        {
            var users = taskEntities.tblTasks.ToList();

            if (users == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);  // HTTPResponseException 
            else
                return Ok(users);
        }

        public IHttpActionResult GetTasks(int id)
        {
            tblTask task = taskEntities.tblTasks.ToList().Where(x => x.QuoteNumber == id).FirstOrDefault();
            if (task == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)  // Another way to use HttpResponseException
                {
                    Content = new StringContent(string.Format("No Task with QuoteNumber = {0}", id)),
                    ReasonPhrase = "QuoteNumber Found"
                };
                throw new HttpResponseException(resp);
                
            }
               
            else
                return Ok(task);
        }

        public IHttpActionResult PostTasks([FromBody] tblTask task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
             {
                var user = taskEntities.tblTasks.Add(task);
                taskEntities.SaveChanges();

                return Ok(user);
            }
        }
        public IHttpActionResult DeleteTasks(int id)
        {
                var user = taskEntities.tblTasks.ToList().FirstOrDefault(x=>x.QuoteNumber == id);
                var  deletedUser=   taskEntities.tblTasks.Remove(user);
                taskEntities.SaveChanges();
           
                return Ok(user);
        }

        // Here use HttpResponse message
        public HttpResponseMessage PutTasks([FromBody] tblTask task)
        {
           var user = taskEntities.tblTasks.ToList().FirstOrDefault(x => x.QuoteNumber == task.QuoteNumber);
          
            if (user != null)
            {
                user.Contact = task.Contact;
                user.QuoteNumber = task.QuoteNumber;
                user.QuoteType = task.QuoteType;
                user.TaskDescription = task.TaskDescription;
                user.TaskType = task.TaskType;
                user.DueDate = task.DueDate;
               
                taskEntities.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }

            else
            { // Follwoing code demostrate  HttpError way to handle exeption
                var message = string.Format("Task with QuoteNuber = {0} not found", task.QuoteNumber);
                HttpError err = new HttpError(message);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
            }
               
            
        }
        [HttpPatch]
        public IHttpActionResult PatchTasks(int id, [FromBody] tblTask task)
        {
            var user = taskEntities.tblTasks.ToList().FirstOrDefault(x => x.QuoteNumber == id);
            if (user != null)
            {
               
                user.TaskDescription = task.TaskDescription;
               

                taskEntities.SaveChanges();
                return Ok(user);
            }

            else
                return BadRequest();

        }
    }
}
