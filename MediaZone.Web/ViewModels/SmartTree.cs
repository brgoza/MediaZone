using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using MediaZone.Data.Entities;
using MediaZone.Data.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace MediaZone.Web.ViewModels;

public class SmartTree
{
    public AppUser User { get; set; } = null!;
   
    internal int BranchCount = 0;
    public IList<SmartTreeBranch> Bramches { get; set; } = new List<SmartTreeBranch>();
    public SmartTreeBranch? CurrentBranch { get; set; }

    public SmartTree(AppUser user, Guid? currentBranchId = null)
    {

        foreach (Folder folder in user.SubscribedFolders.Where(f => f.Parent is null))
        {

            Bramches.Add(new SmartTreeBranch(this, folder)); // this is recursive with the banch constructor...
        }

        CurrentBranch = currentBranchId != null ?
            GetBranch(currentBranchId.Value) : GetBranchByFolderId(user.HomeFolder.Id) ??
            GetBranchByFolderId(user.SubscribedFolders?.FirstOrDefault()?.Id);


}


public SmartTreeBranch? GetBranchByFolderId(Guid? folderId)
{
    if (folderId is null) return null;
    foreach (SmartTreeBranch branch in Bramches)
    {
        if (branch.FolderId == folderId) return branch;
        return GetBranchByFolderId(folderId);
    }
    return null;
}
public SmartTreeBranch? GetBranch(Guid? id)
{
    if (id is null) return null;
    foreach (SmartTreeBranch branch in Bramches)
    {
        if (branch.Id == id) return branch;
        return GetBranch(id);
    }
    return null;
}

}

public class SmartTreeBranch
{

    private readonly SmartTree _tree;
    [JsonProperty("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    private readonly Folder _folder;
    [JsonProperty("folderId")]
    public Guid FolderId => _folder.Id;
    [JsonProperty("selected")]
    public bool IsSelected => _tree.CurrentBranch == this;

    [JsonProperty("label")]
    public string Label => _folder.Name;
    [JsonProperty("items")]
    [JsonPropertyName("items")]
    public IEnumerable<SmartTreeBranch>? Children { get; set; }


    public SmartTreeBranch(SmartTree tree, Folder folder)
    {
        _tree = tree;
        _tree.BranchCount++;
        _folder = folder;
        Children =folder.Children?.Select(ch => new SmartTreeBranch(tree, ch));
    }

}

//public class SmartTreeItem
//{
//    [JsonProperty("label")]
//    public string Label { get; set; } = string.Empty;
//    [JsonProperty("selected")]
//    public bool Selected { get; set; }
//}
