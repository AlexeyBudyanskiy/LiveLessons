using System.Web.Mvc;
using NLog;

namespace LiveLesson.WEB.Filters
{
    public class LogIpFilterAttribute : ActionFilterAttribute
    {
        private readonly ILogger logger;

        public LogIpFilterAttribute()
        {
            logger = DependencyResolver.Current.GetService<ILogger>();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.Url != null)
            {
                logger.Debug(
                    $@"{filterContext.Controller}.{filterContext.ActionDescriptor.ActionName}|Method uri:{filterContext
                        .RequestContext.HttpContext.Request.Url.AbsoluteUri}, " +
                    $@"Client IP: {filterContext.HttpContext.Request.UserHostAddress}, ");
            }
        }
    }
}