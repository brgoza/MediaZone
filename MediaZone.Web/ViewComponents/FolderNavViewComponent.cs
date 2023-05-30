using MediaZone.Data.Entities;
using MediaZone.Data.Entities.Identity;
using MediaZone.Services.Interfaces;
using MediaZone.Web.ViewModels.ViewComponentModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MediaZone.Web.ViewComponents;

public class FolderNavViewComponent : ViewComponent
{
    private readonly IFolderService _folderService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<AppUser> _userManager;
    public FolderNavViewComponent(IFolderService folderService, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
    {
        _folderService = folderService;
        _contextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync(Guid folderId)
    {
        AppUser currentUser = await _userManager.FindByNameAsync(_contextAccessor.HttpContext!.User.Identity!.Name!) ??
            throw new("user is null");
        Folder folder = _folderService.GetFolder(folderId) ?? currentUser.HomeFolder;
        //if (folder is null || currentUser.HomeFolder is null) return View();

        FolderNavViewModel model = new()
        {
            CurrentFolder = folder,
            CurrentFolderId = folder.Id,
            HomeFolder = currentUser.HomeFolder,
            SubscribedFolders = currentUser.SubscribedFolders.ToList(),
            UserId = currentUser.Id,
            SmartTree = new(currentUser)
        };

        return View(model);
    }


}
