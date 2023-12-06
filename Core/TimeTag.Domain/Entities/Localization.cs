using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TimeTag.Domain.Entities
{
    public class Localization : BaseModel
    {
        public string TagName { get; set; }         
        public string Value { get; set; }
        public int rlt_Language_Id { get; set; }
        [ForeignKey(nameof(rlt_Language_Id))]
        public Language Language { get; set; }
    }
    public class Language : BaseModel
    {
        public string LangCode { get; set; }

    }
}
