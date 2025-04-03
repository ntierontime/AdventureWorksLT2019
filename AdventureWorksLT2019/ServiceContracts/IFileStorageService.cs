using Framework.Models;

namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IFileStorageService
    {
        Task<Response<FileModel>> UploadFile(
            FileModel dest, Action<Stream> copyTo);
    }
}

