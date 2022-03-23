using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Shopping.Interface;

namespace Shopping.Repositories
{
    public class BlobHelper : IBlobHelper
    {
        private readonly CloudBlobClient _blobClient;

        public BlobHelper(IConfiguration configuration)
        {
            string keys = configuration["AzureBlob:ConnectionString"];
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(keys);
            _blobClient = storageAccount.CreateCloudBlobClient();
        }

        public async Task<Guid> UploadBlobAsync(IFormFile file, string containerName)
        {
            Stream stream = file.OpenReadStream();
            Guid name = Guid.NewGuid();
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{container}");
            await blockBlob.UploadFromStreamAsync(stream);
            return name;
        }

        public Task<Guid> UploadBlobAsync(byte[] file, string containerName)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UploadBlobAsync(string image, string containerName)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> DeleteBlobAsync(Guid id, string containerName)
        {
            throw new NotImplementedException();
        }
    }
}
