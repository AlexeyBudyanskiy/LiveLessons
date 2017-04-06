using System.Diagnostics;
using System.Web.Mvc;
using NLog;

namespace LiveLesson.WEB.Filters
{
    public class PerfomanceFilterAttribute : FilterAttribute, IActionFilter
    {
        private readonly ILogger logger;
        private Stopwatch stopwatch;

        public PerfomanceFilterAttribute()
        {
            logger = DependencyResolver.Current.GetService<ILogger>();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            stopwatch.Stop();
            if (filterContext.RequestContext.HttpContext.Request.Url != null)
            {
                logger.Debug(
                    $@"{filterContext.Controller}.{filterContext.ActionDescriptor.ActionName}|Perfomance of the method: {filterContext
                        .RequestContext.HttpContext.Request.Url.AbsoluteUri} is {stopwatch.ElapsedMilliseconds} miliseconds");
            }
        }
    }
}