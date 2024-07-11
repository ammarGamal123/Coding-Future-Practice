using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IStudentService
    {
        public Task<List<Student>> GetAllStudentsAsync();

        public Task<Student> GetStudentByIDAsync(int id);

        public Task<string> CreateStudentAsync(Student student);
    }
}
