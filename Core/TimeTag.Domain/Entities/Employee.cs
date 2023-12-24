using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTag.Domain.Enums;

namespace TimeTag.Domain.Entities;
public class Company_Department_Employee : BaseModel
{
    public string NameSurname { get; set; }
    public string Title { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public DateTime BirthDay { get; set; }
    public DateTime? StartedJobTime { get; set; }
    public bool IsActive { get; set; }
    public int? rlt_FileUpload_Id { get; set; }

    public int rlt_Department_Id { get; set; }
    public int? rlt_User_Id { get; set; }

    [ForeignKey(nameof(rlt_FileUpload_Id))]
    public FileUpload ProfileImage { get; set; }



    [ForeignKey(nameof(rlt_Department_Id))]
    public Company_Department Department { get; set; }

    [ForeignKey(nameof(rlt_User_Id))]
    public User User { get; set; }

    public virtual ICollection<Company_Department_Employee_Bank> Banks { get; set; }
    public virtual ICollection<Company_Department_Employee_Log> Logs { get; set; }
    public virtual ICollection<Company_Department_Employee_Token> Tokens { get; set; }

}
public class Company_Department_Employee_Bank : BaseModel
{
    public string Name { get; set; }
    public string OwnerName { get; set; }
    public string Iban { get; set; }
    public int rlt_Employee_Id { get; set; }

    [ForeignKey(nameof(rlt_Employee_Id))]
    public Company_Department_Employee Employee { get; set; }
}
public class Company_Department_Employee_Log : BaseModel
{
    public DateTime ProcessTime { get; set; }
    public LogType Type { get; set; }
    public string IpAddress { get; set; }
    public string Token { get; set; }
    public int rlt_Employee_Id { get; set; }

    [ForeignKey(nameof(rlt_Employee_Id))]
    public Company_Department_Employee Employee { get; set; }
}

public class Company_Department_Employee_Token : BaseModel
{
    public string Token { get; set; }
    public int? rlt_Employee_Id { get; set; }

    public int rlt_Licance_Id { get; set; }

    [ForeignKey(nameof(rlt_Employee_Id))]
    public Company_Department_Employee Employee { get; set; }

    [ForeignKey(nameof(rlt_Licance_Id))]
    public Licance Licance { get; set; }
}

