using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.HelperModels
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : Controller
    {
        protected readonly HttpClient client = new HttpClient();

        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);
            ViewData["Authorization"] = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        }
    }
}
