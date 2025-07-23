using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Bulky.Core.Contracts.Ports.BlobStorage;
using Microsoft.AspNetCore.Http;

namespace Bulky.BlobService.Adapter
{
	public sealed class BlobService : IBlobStorage
	{
		private readonly int MAX_SIZE = 2_097_152;
		
		private readonly BlobServiceClient blobServiceClient;

		public BlobService(BlobServiceClient blobServiceClient)
		{
			this.blobServiceClient = blobServiceClient;
		}


		public async Task<bool> DeleteAsync(string container, string blob)
		{
			var blobContainerClient = blobServiceClient.GetBlobContainerClient(container);

		 	return await blobContainerClient.DeleteBlobIfExistsAsync(blob);
		}

		public async Task<string> UploadAsync(IFormFile file, string container)
		{
			if (file is null)
				throw new NullReferenceException("File must have a value");

			if (file.Length > MAX_SIZE)
				throw new InsufficientMemoryException("File must be 2mb maximum");

			var blobContainerClient = blobServiceClient.GetBlobContainerClient(container);

			await blobContainerClient.CreateIfNotExistsAsync();

			string fileName = Path.Combine(file.Name,"_", new Guid().ToString(), ".", Path.GetExtension(file.FileName));

			using var stream = file.OpenReadStream();

			var response = await blobContainerClient.UploadBlobAsync(fileName, stream);

			return fileName;
		}
	}
}
