using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Core.Domain.Entities
{
	public class Address
	{
		public string UserId { get; set; }
		public string City { get; set; }
		public string PostalCode { get; set; }
		public string StreetAddress { get; set; }
		public string State { get; set; }
	}
}
