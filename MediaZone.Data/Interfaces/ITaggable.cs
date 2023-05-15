using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaZone.Data.Entities.JoinEntities;

namespace MediaZone.Data.Interfaces;

public interface ITaggable 

{
    public Guid Id { get; set; }
    
}
