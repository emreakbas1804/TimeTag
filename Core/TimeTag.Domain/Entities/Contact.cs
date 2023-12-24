using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTag.Domain.Entities;
public class Contact : BaseModel
{
    public string NameSurname { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }    
}