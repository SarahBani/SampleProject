using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Models
{
    public class CustomUnauthorizedResult : JsonResult
    {

        public CustomUnauthorizedResult(string message)
            : base(message)
        {
            base.StatusCode = StatusCodes.Status401Unauthorized;
        }

    }
}
