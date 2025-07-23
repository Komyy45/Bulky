using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bulky.BlobService
{
	public static class DependecnyInjection
	{
		public static IServiceCollection AddBlobStorageServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton(_ => new BlobServiceClient(configuration.GetConnectionString("AzureStorage")));

			return services;
		}
	}
}
