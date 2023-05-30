using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaZone.Data.Entities.Identity;
using MediaZone.Data.Entities.JoinEntities;
using MediaZone.Data.Interfaces;

namespace MediaZone.Data.Entities;

[Table("Folders")]
public class Folder :ITaggable
{
    [Key]
    public Guid Id { get; set; }
  
    [Required, StringLength(255)]
    public string Name { get; set; } = null!;

    public virtual AppUser Owner { get; set; } = null!;
    public Guid OwnerId { get; set; }

    public bool? IsPublic { get; set; } = null!;
  
    public virtual IEnumerable<AppUser> AllowedUsers { get; set; } = null!;
    public virtual IEnumerable<FolderShare> FolderShares { get; set; } = null!; //join entity
    public virtual IEnumerable<AppUser> Subscribers { get; set; } = null!;
    public virtual IEnumerable<FolderSubscription> FolderSubscriptions { get; set; } = null!; //join entity
    public virtual Folder? Parent { get; set; }
    public Guid? ParentId { get; set; }
    [NotMapped]
    public virtual IEnumerable<Folder> Children { get; set; } = null!;
    public virtual IEnumerable<Image> Images { get; set; } = null!;
    public virtual IEnumerable<FolderTag> Tags { get;} = null!;
    public virtual IEnumerable<FolderTagJoinModel> FoldersTags { get; set; } = null!;
}
