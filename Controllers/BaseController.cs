using courses_registration.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace courses_registration.Controllers
{
    public enum HttpStatusCode
    {
        OK = 200,
        NoContent = 204,
        BadRequest = 400,
        NotFound = 404,
        InternalServerError=500
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

                case HttpStatusCode.NoContent:
                    return Ok(response);
                
                case HttpStatusCode.NotFound:   
                    return NotFound(response); 
                         
                default:
                    return StatusCode((int)httpStatusCode, response);
            }
        }

    }
}
