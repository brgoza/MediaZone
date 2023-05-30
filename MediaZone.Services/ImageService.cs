using MediaZone.Services.Interfaces;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using MediaZone.Common;
using MediaZone.Data.Entities;
using MediaZone.Data.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.Extensions.Azure;
using MediaZone.Data;

namespace MediaZone.Services;



public class ImageService : IImageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly IConfiguration _config;
    private readonly ILogger<ImageService> _logger;
    private readonly BlobContainerClient _imagesBlobContainerClient;
    private readonly ApplicationDbContext _dbContext;
    public ImageService(BlobServiceClient blobServiceClient, IConfiguration config, ILogger<ImageService> logger, ApplicationDbContext context)
    {
        _blobServiceClient = blobServiceClient;
        _config = config;
        _logger = logger;
        _dbContext = context;

        string imagesBlobContainerName = _config.GetSection("BlobContainers:Images").Value ?? throw new("images blob container name undefined");
        _imagesBlobContainerClient = _blobServiceClient.GetBlobContainerClient(imagesBlobContainerName);
        _imagesBlobContainerClient.CreateIfNotExistsAsync(PublicAccessType.None);
    }

    private IEnumerable<ImageUpload> Uploads { get; set; } = Enumerable.Empty<ImageUpload>();
    public IEnumerable<ImageUpload> GetActiveUploads() => Uploads;

    public IEnumerable<object>? GetGalleryImages(Guid folderId)
    {
        Folder? folder = _dbContext.Folders.Find(folderId);
        return folder?.Images.Select(i => new { id = i.Id, url = i.ImageUrl, thumb = i.ImageUrl }) ?? null;
    }

    public IEnumerable<Image>? GetImages(Guid folderId)
    {
        Folder? folder = _dbContext.Folders.Find(folderId);
        return folder?.Images;
    }

    private ImageUpload QueueUpload(Image image)
    {
        ImageUpload upload = new() { Image = image };
        Uploads.ToList().Add(upload);
        return upload;
    }

    private static string GetFileExtension(string filename)
    {
        FileInfo fileInfo = new(filename);
        return fileInfo.Extension;
    }
    public async Task<Result<Uri>> UploadImage(Image image, Stream stream)
    {
        _dbContext.Images.Add(image);
        try
        {
            BlobClient blobClient = _imagesBlobContainerClient.GetBlobClient($"{image.ShortId}{GetFileExtension(image.OriginalFilename)}");
            image.SizeInBytes = stream.Length;
            ImageUpload upload = QueueUpload(image);

            var newBlob = await blobClient.UploadAsync(stream, new BlobUploadOptions
            {
                ProgressHandler = new Progress<long>(progress =>
                {
                    UpdateProgress(upload, progress);
                })
            });
            image.ImageUrl = blobClient.Uri.ToString();
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(new Result<Uri>(success: true, $"{image.OriginalFilename} uploaded", blobClient.Uri));
        }
        catch (Exception ex)
        {
            return new Result<Uri>(success: false, $"error uploading {image.OriginalFilename}: {ex.Message}", null);
        }

    }

    private static void UpdateProgress(ImageUpload upload, long progress)
    {
        upload.BytesTransferred = progress;
    }

}