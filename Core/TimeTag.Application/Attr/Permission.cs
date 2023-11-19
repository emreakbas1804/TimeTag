using System;

namespace TimeTag.Application.Attr
{
    public class PermissionAttr : Attribute
    {
        public string Permission { get; set; }
    }
    public static class PermissonTags
    {
        public const string Company = "Company";        
        public const string Department = "Department";
    }
}
