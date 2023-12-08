using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTag.Domain.Enums;

namespace TimeTag.Domain.Entities;
public class Company_Employee : BaseModel
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

    [ForeignKey(nameof(rlt_FileUpload_Id))]
    public FileUpload ProfileImage { get; set; }

  
    
    [ForeignKey(nameof(rlt_Department_Id))]
    public Company_Department Department { get; set; }

    public virtual ICollection<Company_EmployeeBank> Banks { get; set; }
    public virtual ICollection<Company_EmployeeLog> Logs { get; set; }    
    public virtual ICollection<Company_EmployeeToken> Tokens {get;set;}

}
public class Company_EmployeeBank : BaseModel
{
    public string Name { get; set; }
    public string OwnerName { get; set; }
    public string Iban { get; set; }
    public int rlt_Employee_Id { get; set; }

    [ForeignKey(nameof(rlt_Employee_Id))]
    public Company_Employee Employee { get; set; }
}
public class Company_EmployeeLog : BaseModel
{
    public DateTime ProcessTime { get; set; }
    public LogType Type { get; set; }
    public string IpAddress { get; set; }
    public string Token { get; set; }
    public int rlt_Employee_Id { get; set; }

    [ForeignKey(nameof(rlt_Employee_Id))]
    public Company_Employee Employee { get; set; }
}

public class Company_EmployeeToken : BaseModel
{
    public string Token { get; set; }
    public int rlt_Employee_Id { get; set; }
    [ForeignKey(nameof(rlt_Employee_Id))]
    public Company_Employee Employee { get; set; }
}

