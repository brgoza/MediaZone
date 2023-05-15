using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaZone.Data.Entities.Identity;

namespace MediaZone.Data.Entities;

[Table("FolderSubscriptions")]
public class FolderSubscription
{
    public virtual AppUser Subscriber { get; set; } = null!;
    public Guid SubscriberId { get; set; }
    public virtual Folder Folder { get; set; } = null!;
    public Guid FolderId { get; set; }

}
