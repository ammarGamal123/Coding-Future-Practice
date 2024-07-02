using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Entities
{
    public partial class Department
    {
        public Department()
        {
            Students = new HashSet<Student>();
            DepartmentSubjects = new HashSet<DepartmentSubject>();
        }
        [Key]
        public int DeptID { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set;}

    }
}
