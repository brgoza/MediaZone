using MediaZone.Services.Interfaces;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using MediaZone.Util;
using MediaZone.Data.Entities;
using MediaZone.Data.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs.Models;

namespace MediaZone.Services;

public class ImageService : IImageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly IConfiguration _config;
    private readonly ILogger<ImageService> _logger;
    private readonly BlobContainerClient _imagesBlobContainerClient;
    public ImageService(BlobServiceClient blobServiceClient,IConfiguration config, ILogger<ImageService> logger)
    {
        _blobServiceClient = blobServiceClient;
        _config = config;
        _logger = logger;

         string imagesBlobContainerName = _config.GetSection("BlobContainers:Images").Value ?? throw new("images blob container name undefined");
        _imagesBlobContainerClient = _blobServiceClient.GetBlobContainerClient(imagesBlobContainerName);
    }

    //private async Task<Result<
    //>> UploadImage(
    //
    //
    //image, )
    //{
    //    BlobContainerInfo containerInfo = await _imagesBlobContainerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.None);
    //    var blobClient = _imagesBlobContainerClient.GetBlobClient(image.ShortId);
    //    blobClient.UploadAsync()
    //}
}