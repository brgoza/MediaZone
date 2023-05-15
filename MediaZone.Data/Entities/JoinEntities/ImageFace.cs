using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaZone.Data.Entities;

[Table("ImagesFaces")]
public class ImageFace
{
    public virtual Image Image { get; set; } = null!;
    public Guid? ImageId { get; set; }
    public virtual Face Face { get; set; } = null!;
    public Guid? FaceId { get; set; }
    [Range(0, 100)]
    public float Similarity { get; set; }
    
    private bool? confirmed;
    public bool? Confirmed
    {
        get => confirmed;
        set
        {
            confirmed = value;
            if (value == true)
            {
                Similarity = 100;
            }
        }
    }
}
