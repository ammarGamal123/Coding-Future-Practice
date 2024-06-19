using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Entities
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(25)]
        public string Phone { get; set; }


        [ForeignKey("DepartmentID")]
        [InverseProperty("Students")]
        public int DepartmentID { get; set; }


        public virtual Department Departments { get; set; } 
    }
}
