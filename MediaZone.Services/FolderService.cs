using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaZone.Data;
using MediaZone.Data.Entities;
using MediaZone.Data.Entities.Identity;
using MediaZone.Services.Interfaces;
using MediaZone.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MediaZone.Services;

public class FolderService : IFolderService
{
    private readonly ILogger<FolderService> _logger;
    private readonly ApplicationDbContext _dbContext;
    private readonly IIdentityService _identityService;

    public FolderService(ILogger<FolderService> logger, ApplicationDbContext dbContext, IIdentityService identityService)
    {
        _logger = logger;
        _dbContext = dbContext;
        _identityService = identityService;
    }

    public Folder? GetFolder(Guid? id) => _dbContext.Folders.Find(id);
    public IEnumerable<Folder> GetAncestors(Folder folder)
    {
        List<Folder> ancestors = new();
        Folder? parent = folder.ParentFolder;
        while (parent is not null)
        {
            ancestors.Add(parent);
            parent = parent.ParentFolder;
        }
        return ancestors;
    }
    public async Task<Result<Folder>> CreateFolder(string Name, AppUser owner, Folder? parent, bool? isPublic = false)
    {
        Folder newFolder = new()
        {
            Owner = owner,
            Name = Name,
            ParentFolder = parent,
            IsPublic = isPublic,
            Subscribers = new List<AppUser>() { owner },
            AllowedUsers = new List<AppUser>() { owner }
            
        };
       
        try
        {
            _dbContext.Folders.Add(newFolder);
             await _dbContext.SaveChangesAsync();
            _logger.LogInformation("{userName} created new folder {folderName}", owner.UserName, newFolder.Name);
            return new(true, $"created new folder {newFolder.Name}", newFolder);
        }
        catch (Exception ex)
        {
            _logger.LogError("error creating new folder: {exMsg}", ex.Message);
            return new(false, ex.Message, null);
        }
    }
    public async Task<Result> RenameFolder(Folder folder, string newName, AppUser user)
    {
        if (folder.Owner != user && !user.Roles.Contains(await _identityService.GetAdminRole()))
            return new(false, "insufficient access");

        string oldName = folder.Name;
        folder.Name = newName;
        await _dbContext.SaveChangesAsync();
        return new(true, $"folder {oldName} renamed to {newName} by {user.UserName}");
    }
}
