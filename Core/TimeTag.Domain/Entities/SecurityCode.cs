using System;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTag.Domain.Enums;

namespace TimeTag.Domain.Entities;
public class SecurityCode : BaseModel
{
    public DateTime ProcessTime { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string Email { get; set; }
    public int Code { get; set; }
    public SecurityCodeType Type { get; set; }
    public int? rlt_User_Id { get; set; }
    [ForeignKey(nameof(rlt_User_Id))]
    public User User { get; set; }
}