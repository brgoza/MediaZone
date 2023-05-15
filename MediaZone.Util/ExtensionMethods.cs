using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MediaZone.Util;

public static class ExtensionMethods
{
    public static string ToJson(this object obj) => obj.ToJson();
    public static string ToShortGuid(this Guid guid) => Convert.ToBase64String(guid.ToByteArray());
    
}
