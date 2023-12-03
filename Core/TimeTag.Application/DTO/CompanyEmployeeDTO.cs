using System;

namespace TimeTag.Application.DTO;
public class CompanyEmployeeDTO
{
    public int Id { get; set; }
    public string NameSurname { get; set; }
    public string Title { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public DateTime BirthDay { get; set; }
    public DateTime? StartedJobTime { get; set; }    
    public string DepartmentName { get; set; }
    public int DepartmentId { get; set; }
    public string ImageUrl { get; set; }

}