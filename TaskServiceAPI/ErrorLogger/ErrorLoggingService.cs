using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskServiceAPI.DAL_Layer;
using AutoMapper;

namespace TaskServiceAPI.ErrorLogger
{
    public class ErrorLoggingService
    {
        public void InsertErrorLog(ApiError apiError)
        {

              

            try
            {
                using (TaskDBEntities1 tsk = new TaskDBEntities1())
                {

                    DAL_Layer.ErrorLog errorlogs = new ErrorLog();
                    errorlogs.Message = apiError.Message;
                    errorlogs.RequestMethod_ = apiError.RequestMethod;
                    errorlogs.RequestUri_ = apiError.RequestUri;
                    errorlogs.TimeUtc_ = apiError.TimeUtc;
                    tsk.ErrorLogs.Add(errorlogs);
                    tsk.SaveChanges();
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

        }
    }
}