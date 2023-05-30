using MediaZone.Data.Entities;

namespace MediaZone.Web.ViewModels;

public class UploadImageViewModel
{

    public Guid ImageId { get; set; }
    public Guid OwnerId { get; set; }
    public string? Filename { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public Guid FolderId { get; set; }
    public IEnumerable<string> Tags { get; set; } = new List<string>();

}
