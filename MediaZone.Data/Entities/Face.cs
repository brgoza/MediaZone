using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaZone.Data.Entities.Identity;

namespace MediaZone.Data.Entities;

public class Face
{
    [Key]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public virtual AppUser? User { get; set; }
    public virtual IEnumerable<Image> Images { get; set; } = null!;
    public virtual IEnumerable<ImageFace> FacesImages { get; set; } = null!; //join entity

    [Required, StringLength(36)]
    public string RekognitionFaceId { get; set; } = null!;


}
