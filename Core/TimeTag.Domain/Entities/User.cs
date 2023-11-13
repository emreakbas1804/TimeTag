using System;
using System.Collections;
using System.Collections.Generic;
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
    public int rlt_Role_Id { get; set; } 
    [ForeignKey(nameof(rlt_Role_Id))]
    public Role Role { get; set; }
    public virtual ICollection<User_LoginLog> LoginLogs{get;set;}
    public virtual ICollection<User_Token> User_Tokens{get;set;}
    public virtual ICollection<Company> Companies { get; set; }
}

public class Role : BaseModel
{
    public string Name { get; set; }
    public bool IsSystemRole { get; set; } = true;
}

public class User_LoginLog : BaseModel
{
    public string IpAddress { get; set; }        
    public bool IsSuccessLogin { get; set; }
    public string ReferanceUrl { get; set; }
    public int rlt_User_Id { get; set; }
    [ForeignKey(nameof(rlt_User_Id))]
    public User User { get; set; }
}
public class User_Token : BaseModel
{
    public string Name { get; set; }
    public string Value { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int rlt_User_Id { get; set; }
    [ForeignKey(nameof(rlt_User_Id))]
    public User User { get; set; }
}