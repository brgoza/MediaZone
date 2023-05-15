using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaZone.Data.Entities.Identity;
using MediaZone.Data.Entities;
using MediaZone.Util;
using Microsoft.EntityFrameworkCore;

namespace MediaZone.Services.Interfaces;

public interface IFolderService
{
    public Task<Result<Folder>> CreateFolder(string Name, AppUser owner, Folder? parent, bool? isPublic = false);
    public Folder? GetFolder(Guid? id);
    public IEnumerable<Folder> GetAncestors(Folder folder);


}
