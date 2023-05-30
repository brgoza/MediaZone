using System.Runtime.InteropServices;
using MediaZone.Common;
using MediaZone.Common.ExtensionMethods;
using MediaZone.Data.Entities;
using MediaZone.Data.Entities.Identity;
using MediaZone.Services;
using MediaZone.Services.Interfaces;
using MediaZone.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MediaZone.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FolderApiController : ControllerBase
    {
        private readonly ILogger<FolderApiController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFolderService _folderService;

        public FolderApiController(ILogger<FolderApiController> logger, UserManager<AppUser> userManager, IFolderService folderService)
        {
            _logger = logger;
            _userManager = userManager;
            _folderService = folderService;
        }

        [HttpGet("{branchId}")]
        public string GetTree(string? branchId)
        {
            AppUser user = _userManager.FindByNameAsync(User.Identity!.Name!).Result!;
            SmartTree tree = Guid.TryParse(branchId, out Guid id) ? new(user, id) : new(user);
            return tree.Bramches.ToJson();
        }



        [HttpPost(Name = "CreateFolder")]
        [Authorize]
        public async Task<string?> CreateFolder([FromForm] Guid? parentId, [FromForm]string folderName, [FromForm] string? isPublic)
        {
            AppUser? user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            if (user == null) return null;
            bool makePublic = isPublic?.ToUpper() == "ON";
            if (ModelState.IsValid)
            {
                Folder? parent = (parentId is null) ? null : _folderService.GetFolder(parentId.Value);
                var result = await _folderService.CreateFolder(folderName, user, parent, makePublic);
                if (result.Success)
                {
                    _logger.LogInformation("{userName} created new folder {folderName}", user, folderName);
                }
                else
                {
                    _logger.LogError("error creating new folder: {errorMsg}", result.Message);
                }
                return result.Data?.ToJson();

            }
            return null;
        }

    

        // DELETE api/<FolderApi>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
        }
    }
}
