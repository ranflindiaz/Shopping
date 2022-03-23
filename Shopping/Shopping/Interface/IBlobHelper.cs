namespace Shopping.Interface
{
    public interface IBlobHelper
    {
        Task<Guid> UploadBlobAsync(IFormFile file, string containerName);

        Task<Guid> UploadBlobAsync(byte[] file, string containerName);
        
        Task<Guid> UploadBlobAsync(string image, string containerName);
        
        Task<Guid> DeleteBlobAsync(Guid id, string containerName);
    }
}
