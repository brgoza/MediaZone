using System.ComponentModel;
using MediaZone.Data.Entities;
using MediaZone.Data.Entities.Identity;

namespace MediaZone.Web.ViewModels;

public class CreateFolderViewModel
{
    [DisplayName("Folder Name")]
    public string FolderName { get; set; } = string.Empty;
    [DisplayName("Folder parent")]
    public Folder? Parent { get; set; }
    public string ParentName => Parent?.Name ?? "None";
    public Guid? ParentId { get; set; }
    public string ParentOwnerName => Parent?.Owner.FullName ?? "N/A";
    //public AppUser Creator { get; set; } = null!;
    public Guid CreatorId { get; set; }
    public bool IsPublic { get; set; }
    
    
}
