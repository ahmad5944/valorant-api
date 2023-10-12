using Microsoft.AspNetCore.Mvc;
using Valorant.Data;
using Valorant.Models;
using Valorant.Services;

namespace Valorant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FilesController : ControllerBase
    {
        private readonly IFileService _uploadService;

        public FilesController(IFileService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost("PostSingleFile")]
        public async Task<ActionResult> PostSingleFile([FromForm] FileUploadModel fileDetails)
        {
            if (fileDetails is null)
                return BadRequest();
            try
            {
                await _uploadService.PostFileAsync(fileDetails.FileDetails, fileDetails.FileType);  
                return Ok();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost("PostMultipleFiles")]
        public async Task<ActionResult> PostMultipleFiles([FromForm] List<FileUploadModel> fileDetails)
        {
            if (fileDetails == null)
            {
                return BadRequest();
            }

            try
            {
                await _uploadService.PostMultipleFileAsync(fileDetails);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("DownloadFile")]
        public async Task<ActionResult> DownloadFile(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            try
            {
                await _uploadService.DownloadFileById(id);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}