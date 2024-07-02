using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Entities
{
    public class StudentSubject
    {
        [Key]
        public int StudSubID { get; set; }

        [ForeignKey("StudID")]
        public int StudID { get; set; }
        public virtual Student Student { get; set; }

        
        [ForeignKey("SubID")]
        public int SubID { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
