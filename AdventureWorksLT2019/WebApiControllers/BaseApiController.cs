using Framework.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AdventureWorksLT2019.WebApiControllers
{
    public class BaseApiController: Controller
    {
        protected ActionResult ReturnWithoutBodyActionResult(Response serviceResponse)
        {
            if (serviceResponse.Status == HttpStatusCode.OK)
                return Ok();
            else if (serviceResponse.Status == HttpStatusCode.BadRequest)
                return BadRequest(serviceResponse.StatusMessage);
            else if (serviceResponse.Status == HttpStatusCode.NotFound)
                return NotFound(serviceResponse.StatusMessage);
            // else if (result.Status == HttpStatusCode.InternalServerError)

            return StatusCode((int)serviceResponse.Status);
        }

        protected ActionResult<T> ReturnResultOnlyActionResult<T>(Response<T> serviceResponse)
        {
            if (serviceResponse.Status == HttpStatusCode.OK)
                return Ok(serviceResponse.ResponseBody);
            else if (serviceResponse.Status == HttpStatusCode.BadRequest)
                return BadRequest(serviceResponse.StatusMessage);
            else if (serviceResponse.Status == HttpStatusCode.NotFound)
                return NotFound(serviceResponse.StatusMessage);
            // else if (result.Status == HttpStatusCode.InternalServerError)

            return StatusCode((int)serviceResponse.Status);
        }

        protected ActionResult<Response<T>> ReturnActionResult<T>(Response<T> serviceResponse)
        {
            if (serviceResponse.Status == HttpStatusCode.OK)
                return Ok(serviceResponse);
            else if (serviceResponse.Status == HttpStatusCode.BadRequest)
                return BadRequest(serviceResponse.StatusMessage);
            else if (serviceResponse.Status == HttpStatusCode.NotFound)
                return NotFound(serviceResponse.StatusMessage);
            // else if (result.Status == HttpStatusCode.InternalServerError)

            return StatusCode((int)serviceResponse.Status);
        }

        protected ActionResult<PagedResponse<T>> ReturnActionResult<T>(PagedResponse<T> serviceResponse)
        {
            if (serviceResponse.Status == HttpStatusCode.OK)
                return Ok(serviceResponse);
            else if (serviceResponse.Status == HttpStatusCode.BadRequest)
                return BadRequest(serviceResponse.StatusMessage);
            else if (serviceResponse.Status == HttpStatusCode.NotFound)
                return NotFound(serviceResponse.StatusMessage);
            // else if (result.Status == HttpStatusCode.InternalServerError)

            return StatusCode((int)serviceResponse.Status);
        }
    }
}

