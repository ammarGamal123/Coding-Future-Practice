﻿using SchoolProject.Data.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Entities
{
    public class Subject : GeneralLocalizableEntity
    {
        public Subject()
        {
            StudentSubjects = new HashSet<StudentSubject>();
            DepartmentSubjects = new HashSet<DepartmentSubject>();
        }

        [Key]
        public int SubID { get; set; }

        [StringLength(500)]
        public string SubjectNameAr { get; set; }

        [StringLength(500)]
        public string SubjectNameEn { get; set; }


        public DateTime Period { get; set; }

        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }

        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }  

        
    }
}
