
using Valorant.Models;
using Valorant.Data;

namespace Valorant.Services
{
    public interface IFileService
    {
        public Task PostFileAsync(IFormFile fileData, FileType fileType);
        public Task PostMultipleFileAsync(List<FileUploadModel> fileData);
        public Task DownloadFileById(int FileName);
    }
}