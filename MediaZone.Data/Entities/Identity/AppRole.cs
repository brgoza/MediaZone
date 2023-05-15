using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MediaZone.Data.Entities.Identity;

public class AppRole : IdentityRole<Guid>
{

    public virtual IEnumerable<AppUser> Users { get; set; } = null!;
  
}
