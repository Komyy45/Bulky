using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Core.Exceptions.Common
{
	public class NotFoundException : Exception
	{
		public NotFoundException(string entityName) : base($"{entityName} doesn't exists")
		{
			
		}
	}
}
