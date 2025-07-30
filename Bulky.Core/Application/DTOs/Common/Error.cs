using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Core.Application.Models.Common
{
	public sealed record Error(string Code, string Message)
	{
		private static readonly string NotFoundCode = "NotFound";
		private static readonly string ValidationErrorCode = "ValidationError";
		private static readonly string AccountLockedErrorCode = "AccountLockedError";
		private static readonly string AccountNotConfirmedErrorCode = "AccountNotConfirmedError";

		public static readonly Error None = new(string.Empty, string.Empty);
		public static Error NotFound(string message)
		{
			return new Error(NotFoundCode, message);
		}
		public static Error ValidationError(string message)
		{
			return new Error(ValidationErrorCode, message);
		}

		public static Error AccountLockedError(string message)
		{
			return new Error(AccountLockedErrorCode, message);
		}

		public static Error AccountNotConfirmedError(string message)
		{
			return new Error(AccountNotConfirmedErrorCode, message);
		}
	}
}
