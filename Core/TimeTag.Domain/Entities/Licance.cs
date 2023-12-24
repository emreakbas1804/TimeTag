using System.Collections;
using System.Collections.Generic;

namespace TimeTag.Domain.Entities
{
    public class Licance : BaseModel
    {
        public string SerialNumber { get; set; }
        public bool IsAdded { get; set; }
        public Company Company { get; set; }
        public virtual ICollection<Company_Department_Employee_Token> Tokens { get; set; }
    }
}