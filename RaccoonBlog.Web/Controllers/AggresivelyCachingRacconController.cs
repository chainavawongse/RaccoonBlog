using System;
using System.Web.Mvc;

namespace RaccoonBlog.Web.Controllers
{
	public abstract class AggresivelyCachingRacconController : RaccoonController
	{
		IDisposable aggressivelyCacheFor;

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);

			aggressivelyCacheFor = RavenSession.Advanced.DocumentStore.AggressivelyCacheFor(CahceDuration);
		}

		protected abstract TimeSpan CahceDuration { get; }

		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			base.OnActionExecuted(filterContext);

			if (aggressivelyCacheFor != null)
			{
				aggressivelyCacheFor.Dispose();
				aggressivelyCacheFor = null;
			}
		}
	}
}