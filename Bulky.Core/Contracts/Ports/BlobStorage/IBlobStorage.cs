using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Bulky.Core.Contracts.Ports.BlobStorage
{
	public interface IBlobStorage
	{
		public Task<string> UploadAsync(IFormFile file, string container);
		public Task<bool> DeleteAsync(string container, string blob);

	}
}
