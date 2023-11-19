using courses_registration.Helpers;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace courses_registration.Controllers
{
    public enum HttpStatusCode
    {
        OK = 200,
        NoContent = 204,
        BadRequest = 400,
        NotFound = 404,
        InternalServerError=500,
        Conflict = 409,
    }

    
    public class BaseController : ControllerBase
    {
        private readonly Localizer _localizer;

        public BaseController(Localizer localizer)
        {
            _localizer = localizer;
        }

        protected IActionResult Response(HttpStatusCode httpStatusCode, string messageKey, object data = null)
        {

            var response = new
            {
                HttpStatusCode = httpStatusCode,
                Message = _localizer.GetLocalized(messageKey),
                Data = data
            };

            switch (httpStatusCode)
            {

                case HttpStatusCode.BadRequest:
                    return BadRequest(response);

                case HttpStatusCode.OK:
                    return Ok(response);
                
                case HttpStatusCode.NotFound:   
                    return NotFound(response); 
                         
                default:
                    return StatusCode((int)httpStatusCode, response);
            }
        }

    }
}
