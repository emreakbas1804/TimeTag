using System.ComponentModel.DataAnnotations.Schema;
using TimeTag.Core.Domain.Enums;

namespace TimeTag.Domain.Entities;
public class User : BaseModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public Confirm EmailConfirm { get; set; } = Confirm.WaitingForConfirm;
    public string Phone { get; set; }    
}

public class UserRole : BaseModel
{
    public string RoleName { get; set; }
    public bool IsSystemRole { get; set; } = true;
}

public class LoginLog : BaseModel
{
    public string IpAddress { get; set; }
    public string IpLocation { get; set; }
    public string Browser { get; set; }
    public bool IsSuccessLogin { get; set; }
    public string ReferanceUrl { get; set; }
    public int rlt_User_Id { get; set; }
    [ForeignKey(nameof(rlt_User_Id))]
    public User User { get; set; }


}
