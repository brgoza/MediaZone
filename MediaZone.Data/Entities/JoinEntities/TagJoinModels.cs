using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaZone.Data.Interfaces;

namespace MediaZone.Data.Entities.JoinEntities;

[Table("SubjectsTags")]
public abstract class SubjectTagJoinModel
{
    [Key]
    public Guid Id { get; set; }
    public string Discriminator { get; set; } = null!;
    public virtual Tag Tag { get; set; } = null!;

    public Guid TagId { get; set; }
    public string TagText { get; set; } = null!;
    public virtual Guid SubjectId { get; set; }
    //public Type SubjectType { get; init; } = typeof(object);

}
public class ImageTagJoinModel : SubjectTagJoinModel
{
    public virtual Image Image { get; set; } = null!;
    public new virtual ImageTag Tag { get; set; } = null!;

}
public class FolderTagJoinModel : SubjectTagJoinModel
{
    public virtual Folder Folder { get; set; } = null!;
    public new virtual FolderTag Tag { get; set; } = null!;

}