using MediaZone.Data.Entities;
using MediaZone.Data.Entities.Identity;

namespace MediaZone.Web.ViewModels
{
    public class HomeIndexViewModel
    {
        public AppUser CurrentUser { get; set; } = null!;
        public Folder CurrentFolder { get; set; } = null!;
        public Guid CurrentFolderId => CurrentFolder.Id;
        public string? Message { get; set; }
    }
}
