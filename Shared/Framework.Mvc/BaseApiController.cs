using Framework.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Framework.Mvc
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
            if (serviceResponse == null)
                return StatusCode((int)HttpStatusCode.InternalServerError);
            if (serviceResponse.Status == HttpStatusCode.OK)
                return Ok(serviceResponse.ResponseBody);
            if (serviceResponse.Status == HttpStatusCode.BadRequest)
                return BadRequest(serviceResponse.StatusMessage);
            if (serviceResponse.Status == HttpStatusCode.NotFound)
                return NotFound(serviceResponse.StatusMessage);
            // else if (result.Status == HttpStatusCode.InternalServerError)

            return StatusCode((int)serviceResponse.Status);
        }

        protected ActionResult<Response<T>> ReturnActionResult<T>(Response<T> serviceResponse)
        {
            if (serviceResponse == null)
                return StatusCode((int)HttpStatusCode.InternalServerError);
            if (serviceResponse.Status == HttpStatusCode.OK)
                return Ok(serviceResponse);
            if (serviceResponse.Status == HttpStatusCode.BadRequest)
                return BadRequest(serviceResponse.StatusMessage);
            if (serviceResponse.Status == HttpStatusCode.NotFound)
                return NotFound(serviceResponse.StatusMessage);
            // else if (result.Status == HttpStatusCode.InternalServerError)

            return StatusCode((int)serviceResponse.Status);
        }

        protected ActionResult<ListResponse<T>> ReturnActionResult<T>(ListResponse<T> serviceResponse)
        {
            if (serviceResponse == null)
                return StatusCode((int)HttpStatusCode.InternalServerError);
            if (serviceResponse.Status == HttpStatusCode.OK)
                return Ok(serviceResponse);
            if (serviceResponse.Status == HttpStatusCode.BadRequest)
                return BadRequest(serviceResponse.StatusMessage);
            if (serviceResponse.Status == HttpStatusCode.NotFound)
                return NotFound(serviceResponse.StatusMessage);
            // else if (result.Status == HttpStatusCode.InternalServerError)

            return StatusCode((int)serviceResponse.Status);
        }
    }
}

