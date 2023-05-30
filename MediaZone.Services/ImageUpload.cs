using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaZone.Data.Entities;

namespace MediaZone.Services;

public class ImageTransfer
{
    public Image Image { get; set; } = null!;
    
    public long BytesTransferred { get; set; }
    public int ProgressPercentage => (int)(BytesTransferred / Image.SizeInBytes * 100);
}
public class ImageUpload : ImageTransfer { }
public class ImageDownload : ImageTransfer { }