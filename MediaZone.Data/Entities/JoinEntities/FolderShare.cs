using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaZone.Data.Entities.Identity;

namespace MediaZone.Data.Entities.JoinEntities;

[Table("FolderShares")]
public class FolderShare
{
    public virtual Folder Folder { get; set; } = null!;
    public Guid FolderId { get; set; }
    public virtual AppUser AllowedUser { get; set; } = null!;
    public Guid AllowedUserId { get; set; }
}
