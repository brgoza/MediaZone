using System.ComponentModel.DataAnnotations.Schema;
using MediaZone.Data.Entities.JoinEntities;
using Microsoft.AspNetCore.Identity;

namespace MediaZone.Data.Entities.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [NotMapped] public string FullName => $"{FirstName} {LastName}";

        public virtual IEnumerable<AppRole> Roles { get; set; } = null!;
        [NotMapped]
        public  Folder? HomeFolder { get; set; }
        public Guid HomeFolderId { get; set; }
        public virtual IEnumerable<Folder> OwnedFolders { get; set; } = null!;
        public virtual IEnumerable<Folder> SubscribedFolders { get; set; } = null!;
        public virtual IEnumerable<Folder> AllowedFolders { get; set; } = null!;
        
        // Join entities
        public virtual IEnumerable<FolderShare> FolderShares { get; set; } = null!;
        public virtual IEnumerable<FolderSubscription> FolderSubscriptions { get; set; } = null!;
    }
}