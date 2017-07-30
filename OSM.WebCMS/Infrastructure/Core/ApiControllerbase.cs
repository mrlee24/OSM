using System;
using Microsoft.AspNetCore.Mvc;
using OSM.Service;
using Microsoft.EntityFrameworkCore;
using OSM.Model.Entities;
using System.Net;
using System.Net.Http;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OSM.WebCMS.Infrastructure.Core
{
    [Route("api/[controller]")]
    public class ApiControllerBase : Controller
    {
        private IErrorService _errorService;
        public ApiControllerBase(IErrorService errorService)
        {
            this._errorService = errorService;
        }
        
        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage requestMessage = null;
            try
            {
                requestMessage = function.Invoke();
            }
            catch (DbUpdateException dbEx)
            {
                LogError(dbEx);
                //requestMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message);
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                LogError(ex);
                //response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return requestMessage;
        } 

        private void LogError(Exception ex)
        {
            try
            {
                Error error = new Error();
                error.CreatedDate = DateTime.Now;
                error.Message = ex.Message;
                error.StackTrace = ex.StackTrace;
                _errorService.Create(error);
                _errorService.Save();
            }
            catch
            {

            }
        }
    }
}
 
 