using MediaZone.Data.Entities;
using MediaZone.Data.Entities.Identity;
using MediaZone.Services.Interfaces;
using MediaZone.Web.ViewModels.ViewComponentModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MediaZone.Web.ViewComponents
{
    public class FolderActionsViewComponent : ViewComponent
    {
        private IFolderService _folderService;
        private IIdentityService _idService;

        public FolderActionsViewComponent(IFolderService folderService, IIdentityService idService)
        {
            _folderService = folderService;
            _idService = idService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid userId, Guid folderId)
        {

            AppUser? user = await _idService.GetUser(userId);
            Folder? folder = await _folderService.GetFolderAsync(folderId);
            return View(user is null || folder is null ?
                new FolderActionsViewModel("You need to register to do anything!") : new FolderActionsViewModel(folder));
        }
    }
}

