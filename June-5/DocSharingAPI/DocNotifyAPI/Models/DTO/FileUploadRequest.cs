using Microsoft.AspNetCore.Http;

public class FileUploadRequest
{
    public IFormFile File { get; set; }
    //IFormFile is an ASP.NET Core type used to represent uploaded files.
}
