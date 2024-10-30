using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PassLocker.Web.Response;

namespace PassLocker.Web.Filter;

public class SessionVerifier : ActionFilterAttribute
{
	public override void OnActionExecuting(ActionExecutingContext context)
	{
		var cookie = context.HttpContext.Request.Cookies["Access"];
		if (cookie != "Success" && cookie != "Banned")
		{
			context.Result = new JsonResult(new ApiResponse("Unauthorized: Session not started", false))
			{
				StatusCode = 401
			};
			return;
		}
		else if (cookie == "Banned")
		{
			context.Result = new JsonResult(new ApiResponse("Banned: You are banned for 24 hours", false))
			{
				StatusCode = 401
			};
			return;
		}
		base.OnActionExecuting(context);
	}

	public override void OnActionExecuted(ActionExecutedContext context)
	{
		base.OnActionExecuted(context);
	}
}