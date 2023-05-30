using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaZone.Data.Entities;
using MediaZone.Common;

namespace MediaZone.Services.Interfaces;

public interface IImageService
{
    public IEnumerable<ImageUpload> GetActiveUploads();
    public Task<Result<Uri>> UploadImage(Image image, Stream stream);
    public IEnumerable<Image>? GetImages(Guid folderId);
    public IEnumerable<object>? GetGalleryImages(Guid folderId);




}
