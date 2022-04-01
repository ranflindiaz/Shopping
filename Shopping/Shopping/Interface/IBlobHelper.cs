namespace Shopping.Interface
{
    public interface IBlobHelper
    {
        Task DeleteBlobAsync(Guid id, string containerName);
        Task<Guid> UploadBlobAsync(IFormFile file, string containerName);
        Task<Guid> UploadBlobAsync(byte[] file, string containerName);
        Task<Guid> UploadBlobAsync(string image, string containerName);
    }
}
