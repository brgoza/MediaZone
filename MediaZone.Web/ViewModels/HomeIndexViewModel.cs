using MediaZone.Data.Entities.Identity;

namespace MediaZone.Web.ViewModels
{
    public class HomeIndexViewModel
    {
        public AppUser? CurrentUser { get; set; } 
        public Guid? CurrentFolderId { get; set; }
        public string? Message { get; set; }
    }
}
