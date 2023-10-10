using DocConnect.Business.Models.Enums;
using DocConnect.Business.Models.Results;
using Microsoft.AspNetCore.Mvc;

namespace DocConnect.Presentation.API.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult HandleResponse<T>(this ControllerBase controller, IResult<T> response)
        {
            if (response.StatusCode == DocConnectStatusCode.NotFound)
            {
                return controller.NotFound(response.ErrorMessages);
            }
            else if (response.StatusCode == DocConnectStatusCode.BadRequest)
            {
                return controller.BadRequest(response.ErrorMessages);
            }
            else if (response.StatusCode == DocConnectStatusCode.Unauthorized)
            {
                return controller.Unauthorized(response.ErrorMessages);
            }
            else if (response.StatusCode == DocConnectStatusCode.OK)
            {
                return controller.Ok(response.Data);
            }
            else if (response.StatusCode == DocConnectStatusCode.NoContent)
            {
                return controller.NoContent();
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
