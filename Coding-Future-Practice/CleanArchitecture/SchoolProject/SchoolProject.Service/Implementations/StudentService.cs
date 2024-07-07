using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await  _studentRepository.GetStudentsAsync(); 
        }

        public async Task<Student> GetStudentByIDAsync(int id)
        {
            var student = await _studentRepository.GetTableNoTracking()
                                            .Include(s => s.Department)
                                            .Where(s => s.StudID.Equals(id))
                                            .FirstOrDefaultAsync();

            return student;
        }
    }
}
