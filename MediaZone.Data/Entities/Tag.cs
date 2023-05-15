using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaZone.Data.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MediaZone.Data.Entities;

[Table("Tags")]
public abstract class Tag
{
    [Key]
    public Guid Id { get; set; }
    public string TagText { get; set; } = null!;
    public string Discriminator { get; set; } = null!;
    public abstract Guid SubjectId { get; set; }
  
}


public class ImageTag : Tag
{
    public virtual IEnumerable<Image> Images { get; set; } = null!;
    public override  Guid SubjectId { get; set; }
    //public Guid ImageId { get; set; }
    
    public virtual IEnumerable<JoinEntities.ImageTagJoinModel> ImageTags { get; set; } = null!;
    
}

public class FolderTag : Tag
{
    public virtual IEnumerable<Folder> Folders { get; set; } = null!;
    public override Guid SubjectId { get; set; }


    //public Guid FolderId { get; set; }
    public virtual IEnumerable<JoinEntities.FolderTagJoinModel> FolderTags { get; set; } = null!;

    
}
