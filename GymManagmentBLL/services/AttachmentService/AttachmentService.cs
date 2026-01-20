
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace GymManagementBLL.services.AttachmentService;

public class AttachmentService : IAttachmentService
{
    public AttachmentService(IWebHostEnvironment webHost)
    {
        _webHost = webHost;
    }

    private readonly string[] allowedExtensions = { ".jpg", ".png", ".jpeg" };
    private readonly long maxFileSize = 5 * 1024 * 1024;
    private readonly IWebHostEnvironment _webHost;

    public string? Upload(string folderName, IFormFile file)
    {
        if(folderName is null || file is null || file.Length == 0) return null;
        if(file.Length > maxFileSize) return null;
        //get extension
        var extension = Path.GetExtension(file.FileName).ToLower();
        if (!allowedExtensions.Contains(extension)) return null;
        // get file path
        var folderPath = Path.Combine(_webHost.WebRootPath, "images", folderName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        //uniqe file name
        var fileName = Guid.NewGuid().ToString() + extension;

        var filePath = Path.Combine(folderPath, fileName);
        using var fileStream = new FileStream(filePath, FileMode.Create);

        file.CopyTo(fileStream);

        return fileName;

    }

    public bool Delete(string fileName, string folderName)
    {
        if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(folderName)) return false;

        var fullPath = Path.Combine(_webHost.WebRootPath, "images", folderName, fileName);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            return true;
        }
       return false;
    }

   
}
