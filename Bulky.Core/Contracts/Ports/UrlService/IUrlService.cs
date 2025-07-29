using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Core.Contracts.Ports.UrlService
{
	public interface IUrlService
	{
		public string BuildUrl(string action, string controller, string area, object? queryParams = null);
	}
}
