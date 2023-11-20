using System;
using System.ComponentModel.DataAnnotations;

namespace TimeTag.Application.Abstractions
{
    public class AddEmployeeDTO
    {
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public string NameSurname { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime? StartedJobTime { get; set; }        
    }

}