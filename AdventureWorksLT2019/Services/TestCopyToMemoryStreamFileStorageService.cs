using Framework.Models;
using AdventureWorksLT2019.ServiceContracts;

namespace AdventureWorksLT2019.Services
{
    public class TestCopyToMemoryStreamFileStorageService: IFileStorageService
    {
        public async Task<Response<FileModel>> UploadFile(
            FileModel dest, Action<Stream> copyTo)
        {
            // this is a test
            using (MemoryStream memStream = new MemoryStream(dest.Length))
            {
                copyTo(memStream);
            }

            var response = new Response<FileModel> { Status = System.Net.HttpStatusCode.OK, ResponseBody = dest };
            response.ResponseBody.Uri = "testfolder/test.image";
            return await Task.FromResult(response);
        }
    }
}

