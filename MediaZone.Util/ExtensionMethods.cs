using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MediaZone.Common.ExtensionMethods;

public static class ExtensionMethods
{
    public static string ToJson(this object obj)
    {
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        return JsonConvert.SerializeObject(obj,settings);
        
    }
    public static string ToShortGuid(this Guid guid)
    {
        string base64String = Convert.ToBase64String(guid.ToByteArray());
        base64String.Replace("/",null);
        base64String.Replace("\\",null);
        int indexOf = base64String.IndexOf("=");
        
        return (indexOf == -1) ? base64String : base64String[..indexOf];
    }

}
