using System.Diagnostics;
using MediaZone.Data.Entities;
using MediaZone.Data.Entities.Identity;
using MediaZone.Services.Interfaces;
using MediaZone.Util;
using MediaZone.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaZone.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IIdentityService _identityService;
        private readonly IFolderService _folderService;

        public HomeController(ILogger<HomeController> logger, IIdentityService identityService, IFolderService folderService)
        {
            _logger = logger;
            _identityService = identityService;
            _folderService = folderService;
        }

        private async Task<AppUser> GetCurrentUserAsync() => await _identityService.GetUser(User.Identity!.Name!) ?? throw new("error getting current user");
        public async Task<IActionResult> Index(Guid? folderId)
        {
            AppUser user = await GetCurrentUserAsync();

            HomeIndexViewModel model = new() { CurrentUser = user, CurrentFolderId = folderId ?? user.HomeFolderId };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateFolder()
        {
            AppUser user = await GetCurrentUserAsync();
            CreateFolderViewModel model = new()
            {
                CreatorId = user.Id
               
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateFolder(CreateFolderViewModel model)
        {
            AppUser user = await _identityService.GetUser(model.CreatorId) ?? throw new("Can't find user");
            Folder? parent = _folderService.GetFolder(model.ParentId); 
            if (ModelState.IsValid)
            {
                var result = await _folderService.CreateFolder(model.FolderName, user, parent, model.IsPublic);
                if (result.Success)
                {
                    _logger.LogInformation("{userName} created new folder {folderName}",user, model.FolderName);

                }
                else
                {
                    _logger.LogError("error creating new folder: {errorMsg}", result.Message);
                }
                return RedirectToAction("Index", new { folderId = result.Data?.Id });
            }
            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}