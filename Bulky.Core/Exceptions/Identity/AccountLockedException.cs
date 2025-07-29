using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Core.Exceptions.Identity
{
	public class AccountLockedException : Exception
	{
		public AccountLockedException() : base("This Account is currently locked try again in a while")
		{
			
		}

		public AccountLockedException(string message) : base(message)
 		{
			
		}
	}
}
