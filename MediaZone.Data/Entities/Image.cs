using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaZone.Data.Entities.Identity;
using MediaZone.Data.Entities.JoinEntities;
using MediaZone.Data.Interfaces;
using MediaZone.Util;

namespace MediaZone.Data.Entities;

[Table("Images")]
public class Image :ITaggable
{
   public Guid Id { get; set; }
    [NotMapped] public string ShortId => Id.ToShortGuid();
    public virtual AppUser Owner { get; set; } = null!;
    public Guid OwnerId { get; set; }


    [Required]
    [DataType(DataType.ImageUrl)]
    public string ImageUrl { get; set; } = null!;

    [Required, StringLength(255)]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public virtual IEnumerable<ImageTag> Tags { get; set; } = null!;
    public virtual IEnumerable<ImageTagJoinModel> ImagesTags { get; set; } = null!;
    public virtual IEnumerable<Face> Faces { get; set; } = null!;
    public virtual IEnumerable<ImageFace> ImagesFaces { get; set; } = null!; //join entity
    public virtual Folder Folder { get; set; } = null!;

    public Guid FolderId { get; set; }



}
