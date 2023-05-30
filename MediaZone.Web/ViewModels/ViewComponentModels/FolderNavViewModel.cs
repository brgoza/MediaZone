using System.Xml;
using MediaZone.Data.Entities;
using Newtonsoft.Json;

namespace MediaZone.Web.ViewModels.ViewComponentModels;

public class FolderNavViewModel
{

    public Guid UserId { get; set; }
    public Folder CurrentFolder { get; set; } = null!;
    public Guid CurrentFolderId { get; set; }
    public Folder HomeFolder { get; set; } = null!;
    public SmartTree SmartTree { get; set; } = null!;
    public IList<Folder> SubscribedFolders { get; set; } = null!;
}

    

