using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTag.Domain.Entities;
public class Company_Employee : BaseModel
{
    public string NameSurname { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public DateTime BirthDay { get; set; }
    public DateTime? StartedJobTime { get; set; }
    public int IsActive { get; set; }
    public string LoginTag { get; set; }

    public int rlt_FileUpload_Id { get; set; }
    public int rlt_Company_Id { get; set; }
    public int rlt_Department_Id { get; set; }

    [ForeignKey(nameof(rlt_Department_Id))]
    public Company_Department Department { get; set; }

    [ForeignKey(nameof(rlt_FileUpload_Id))]
    public FileUpload ProfileImage { get; set; }

    [ForeignKey(nameof(rlt_Company_Id))]
    public Company Company { get; set; }

    public ICollection<Company_EmployeeBank> Banks { get; set; }

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
public class Company_EmployeeLoginJob : BaseModel
{
    public DateTime LoginTime { get; set; }
    public string IpAddress { get; set; }
    public string Token { get; set; }
    public int rlt_Employee_Id { get; set; }
    
    [ForeignKey(nameof(rlt_Employee_Id))]
    public Company_Employee Employee { get; set; }
}
public class Company_EmployeeLogOutJob : BaseModel
{
    public DateTime LogoutTime { get; set; }
    public string IpAddress { get; set; }
    public string Token { get; set; }
    public int rlt_Employee_Id { get; set; }
   
    [ForeignKey(nameof(rlt_Employee_Id))]
    public Company_Employee Employee { get; set; }
}
