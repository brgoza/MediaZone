using MediaZone.Data.Entities;
using MediaZone.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediaZone.Web.ViewComponents
{
    internal class FolderContentsViewModel
    {
        public Folder Folder { get; set; }
        public Guid FolderId => Folder.Id;
        public string FolderName { get; set; } 
        public string OwnerName => $"{Folder.Owner.FirstName} {Folder.Owner.LastName}";
        public IEnumerable<Folder>? Ancestors { get; set; }
        public IEnumerable<Folder>? ChildFolders => Folder.ChildFolders;
        public IEnumerable<Image>? Images => Folder.Images;
        internal FolderContentsViewModel(Folder folder)
        {
            Folder = folder;
            FolderName = folder.Name;
        }
    }
    public class FolderContentsViewComponent : ViewComponent
    {

        private readonly IFolderService _folderService;
        private readonly IHttpContextAccessor _contextAccessor;

        public FolderContentsViewComponent(IFolderService folderService, IHttpContextAccessor contextAccessor)
        {

            _folderService = folderService;
            _contextAccessor = contextAccessor;
        }
        public IViewComponentResult Invoke(Guid folderId)
        {
            Folder folder = _folderService.GetFolder(folderId);
            FolderContentsViewModel model = CreateViewModel(folder);
            return View(model);
        }

        private FolderContentsViewModel CreateViewModel(Folder folder) => new(folder)
        {
            Ancestors = _folderService.GetAncestors(folder),
        };

    }
}
