using Microsoft.EntityFrameworkCore;

using Valorant.Data;

namespace Valorant.Services
{
    public class FileService : IFileService
    {
        private readonly DataContext dataContext;
        public FileService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task PostFileAsync(IFormFile fileData, FileType fileType)
        {
            try
            {
                var fileDetails = new FileDetails()
                {
                    Id = 0,
                    FileName = fileData.FileName,
                    FileType = fileType
                };

                using (var stream = new MemoryStream())
                {
                    fileData.CopyTo(stream);
                    fileDetails.FileData = stream.ToArray();
                }

                var result = dataContext.FileDetails.Add(fileDetails);
                await dataContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task PostMultipleFileAsync(List<FileUploadModel> fileData)
        {
            try
            {
                foreach (FileUploadModel file in fileData)
                {
                    var fileDetails = new FileDetails()
                    {
                        Id = 0,
                        FileName = file.FileDetails.FileName,
                        FileType = file.FileType
                    };

                    using (var stream = new MemoryStream())
                    {
                        file.FileDetails.CopyTo(stream);
                        fileDetails.FileData = stream.ToArray();
                    }

                    var result = dataContext.FileDetails.Add(fileDetails);
                }
                await dataContext.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task DownloadFileById(int id)
        {
            try
            {
                var file = dataContext.FileDetails.Where(x => x.Id == id).FirstOrDefaultAsync();

                var content = new System.IO.MemoryStream(file.Result.FileData);
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(), "FileDownloaded",
                    file.Result.FileName
                );
                await CopyStream(content, path);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task CopyStream(Stream stream, string downloadPath)
        {
            using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream);
            }
        }
    }
}