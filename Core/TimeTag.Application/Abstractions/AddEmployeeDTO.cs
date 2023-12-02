using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

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
        public int? rlt_FileUpload_Id { get; set; }
        public IFormFile Photo { get; set; }
        public DateTime? StartedJobTime { get; set; }        
    }

}