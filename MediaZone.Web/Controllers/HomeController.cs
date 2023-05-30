using System.Diagnostics;
using MediaZone.Data.Entities;
using MediaZone.Data.Entities.Identity;
using MediaZone.Services.Interfaces;
using MediaZone.Common;
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



        [Authorize]
        public async Task<IActionResult> Index(Guid? folderId)
        {
            AppUser user = await GetCurrentUserAsync();
            if (!user.SubscribedFolders.Any())
            {
                var result = await _folderService.CreateFolder($"{user.FullName}'s home folder", user, null, true);
                await _folderService.SetHomeFolder(user, result.Data!);
            }
            Folder folder = folderId is not null ? await _folderService.GetFolderAsync(folderId.Value) ?? user.HomeFolder : user.HomeFolder;
            HomeIndexViewModel model = new()
            {
                CurrentUser = user,
                CurrentFolder = folder
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult GetFolderContents(Guid folderId)
        {
            return ViewComponent("FolderContents", folderId);
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private async Task<AppUser> GetCurrentUserAsync() => await _identityService.GetUser(User.Identity!.Name!) ?? throw new("error getting current user");

    }
}