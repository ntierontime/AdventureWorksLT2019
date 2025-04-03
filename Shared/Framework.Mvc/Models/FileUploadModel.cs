using Microsoft.AspNetCore.Http;

namespace Framework.Mvc.Models
{
    public class FileUploadModel
    {
        public IFormFile FileDetails { get; set; } = null!;
    }
}

