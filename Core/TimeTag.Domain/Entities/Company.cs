using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TimeTag.Domain.Enums;

namespace TimeTag.Domain.Entities
{
    public class Company : BaseModel
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string WebSite { get; set; }
        public int rlt_User_Id { get; set; }
        public int? rlt_FileUpload_Id { get; set; }
        public int rlt_Licance_Id { get; set; }


        [ForeignKey(nameof(rlt_FileUpload_Id))]
        public virtual FileUpload Logo { get; set; }

        [ForeignKey(nameof(rlt_User_Id))]
        public virtual User Owner { get; set; }

        [ForeignKey(nameof(rlt_Licance_Id))]
        public virtual Licance Licance { get; set; }

        public virtual ICollection<Company_Department> Departments { get; set; }             
    }

    public class Company_Department : BaseModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string StartJobTime { get; set; }
        public string FinishJobTime { get; set; }
        public int rlt_Company_Id { get; set; }

        [ForeignKey(nameof(rlt_Company_Id))]
        public Company Company { get; set; }
        public virtual ICollection<Company_Department_Employee> Employees { get; set; }
    }









}