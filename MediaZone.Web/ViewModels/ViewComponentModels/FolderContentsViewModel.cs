using MediaZone.Data.Entities;

namespace MediaZone.Web.ViewModels.ViewComponentModels
{
    public class FolderContentsViewModel
    {
        public Folder Folder { get; set; } = null!;
        public string FolderName { get; set; }
        public string OwnerName { get; set; }
        public Guid FolderId { get; set; }
        public FolderContentsViewModel(Folder folder)
        {
            Folder = folder; FolderName = folder.Name; OwnerName = folder.Owner.FullName;
        }

    }
}
