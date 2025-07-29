using Bulky.Core.Contracts.Ports.UrlService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Bulky.Web.Services
{
	public class UrlService : IUrlService
	{
		private readonly IUrlHelper _urlHelper;

		public UrlService(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
		{ 
			_urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext!);
		}

		public string BuildUrl(string action, string controller, string area, object? queryParams = null)
		{
			var routeValues = new RouteValueDictionary(queryParams)
			{
				["area"] = area
			};

			return _urlHelper.Action(action, controller, routeValues)!;
		}
	}
}
