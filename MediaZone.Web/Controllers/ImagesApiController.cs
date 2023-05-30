using MediaZone.Data.Entities;
using MediaZone.Data.Entities.Identity;
using MediaZone.Services;
using MediaZone.Services.Interfaces;
using MediaZone.Common.ExtensionMethods;
using MediaZone.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MediaZone.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MediaZone.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesApiController : ControllerBase
    {
        private readonly ILogger<ImagesApiController> _logger;
        private readonly IImageService _imageService;
        private readonly IFolderService _folderService;
        private readonly UserManager<AppUser> _userManager;
        public ImagesApiController(ILogger<ImagesApiController> logger, IImageService imageService, IFolderService folderService, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _imageService = imageService;
            _folderService = folderService;
            _userManager = userManager;
        }

        [HttpGet]
        public string GetUploadProgress() => _imageService.GetActiveUploads().ToJson();

        [HttpGet]
        public string GetUploadProgress(Guid imageId) => _imageService.GetActiveUploads().First(u => u.Image.Id == imageId).ToJson();

        [HttpGet("{folderId}")]
        public string GetImages([FromQuery] Guid folderId)
        {
            string? results = _imageService.GetGalleryImages(folderId)?.ToJson();
            return results;
        }
        
        [HttpPost]
        public async Task<Result<Uri>> Upload( [FromForm]string folderId, [FromForm] IFormFile file_data)
        {
            if (file_data is null || file_data.Length == 0) { return new(false, "no file", null); }
            AppUser? currentUser = await _userManager.FindByNameAsync(User.Identity?.Name ?? "");
            if (currentUser is null) { return new(false, "not logged in", null); }
            Folder? folder = _folderService.GetFolder(Guid.Parse( folderId));
            if (folder is null) { return new Result<Uri>(success: false, "folder is null", null); }
            Image newImage = new()
            {
                SizeInBytes = file_data.Length,
                Owner = currentUser,
                Folder = folder,
                OriginalFilename = file_data.FileName,
            };

          return await _imageService.UploadImage(newImage, file_data.OpenReadStream());
        }




       
        // DELETE api/<ImagesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
