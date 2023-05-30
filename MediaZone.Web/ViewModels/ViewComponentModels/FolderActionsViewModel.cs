using MediaZone.Data.Entities;
using MediaZone.Data.Entities.Identity;

namespace MediaZone.Web.ViewModels.ViewComponentModels
{
    public class FolderActionsViewModel
    {
        public string? Message { get; set; } = null;
        public Guid FolderId { get; set; } = Guid.Empty;
        public string FolderName { get; set; } = string.Empty;
        public Guid FolderOwnerId { get; set; } = Guid.Empty;
        public string FolderOwnerName { get; set; } = string.Empty;

        public FolderActionsViewModel(string message)
        {
            Message = message;
        }
        public FolderActionsViewModel(Folder folder)
        {
            FolderId = folder.Id;
            FolderOwnerId = folder.OwnerId;
            FolderOwnerName = folder.Owner.FullName;
            FolderName = folder.Name;
        }
    }
}
