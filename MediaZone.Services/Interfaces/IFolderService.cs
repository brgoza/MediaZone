using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaZone.Data.Entities.Identity;
using MediaZone.Data.Entities;
using MediaZone.Common;
using Microsoft.EntityFrameworkCore;

namespace MediaZone.Services.Interfaces;

public interface IFolderService
{
    public Task<Result<Folder>> CreateFolder(string Name, AppUser owner, Folder? parent, bool? isPublic = false);
    public Task<Result> SetHomeFolder(AppUser user, Folder folder);
    public Folder? GetFolder(Guid id);
    public Task<Folder?> GetFolderAsync(Guid id);
    public IEnumerable<Folder> GetAncestors(Folder folder);


}
