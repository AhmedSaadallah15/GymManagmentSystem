

using Microsoft.AspNetCore.Http;


namespace GymManagementBLL.services.AttachmentService;

public interface IAttachmentService
{

    string? Upload(string folderName, IFormFile file);

    bool Delete(string fileName,string folderName );
}
