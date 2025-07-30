using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Core.Ports.Out
{
	public interface IUrlService
	{
		public string BuildUrl(string action, string controller, string area, object? queryParams = null);
	}
}
