using Azure.Storage.Blobs;
using Bulky.Core.Ports.Out;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bulky.BlobService
{
	public static class DependecnyInjection
	{
		public static IServiceCollection AddBlobStorageServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton(_ => new BlobServiceClient(configuration.GetConnectionString("AzureStorage")));

			services.AddSingleton<IBlobStorage, BlobStorage.Adapter.BlobService>();

			return services;
		}
	}
}
