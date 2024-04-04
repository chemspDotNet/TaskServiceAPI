using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace TaskServiceAPI.CustomFilters
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
      
            public override void Handle(ExceptionHandlerContext context)
            {
                if (context.Exception is ValidationException)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent(context.Exception.Message),
                        ReasonPhrase = "ValidationException"
                    };

                    context.Result = new ErrorMessageResult(context.Request, resp);
                }
                else
                {
                // Do something here...
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = "UnKoown"
                };

                context.Result = new ErrorMessageResult(context.Request, resp);
            }
            }

            public class ErrorMessageResult : IHttpActionResult
            {
                private HttpRequestMessage _request;
                private HttpResponseMessage _httpResponseMessage;


                public ErrorMessageResult(HttpRequestMessage request, HttpResponseMessage httpResponseMessage)
                {
                    _request = request;
                    _httpResponseMessage = httpResponseMessage;
                }

                public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
                {
                    return Task.FromResult(_httpResponseMessage);
                }
            }
        }
    }
    
      
   
 
