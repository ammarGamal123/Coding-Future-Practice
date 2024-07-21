﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<string> CreateStudentAsync(Student student)
        {
            // Check if name exists
            var isNameExists = _studentRepository.GetTableNoTracking()
                              .FirstOrDefault(s => s.Name == student.Name);

            if (isNameExists != null)
            {
                return "Name is already exists";
            }

            await _studentRepository.AddAsync(student);

            return "Added Successfully";
        }

        public async Task<string> DeleteStudentAsync(Student student)
        {
            var trans = _studentRepository.BeginTransaction();

            try
            {
                await _studentRepository.DeleteAsync(student);
                await trans.CommitAsync();
                return "Success";
            }
            catch
            {
                await trans.RollbackAsync();
                return "Failed";
            }
       }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await  _studentRepository.GetStudentsAsync(); 
        }

        public async Task<Student> GetStudentWithIncludingByIDAsync(int id)
        {
            var student = await _studentRepository.GetTableNoTracking()
                                            .Include(s => s.Department)
                                            .Where(s => s.StudID.Equals(id))
                                            .FirstOrDefaultAsync();

            return student;
        }

        public async Task<Student> GetStudentByIDAsync(int id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        public async Task<bool> IsNameExists(string name)
        {
            return await _studentRepository.GetTableNoTracking()
                .AnyAsync(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> IsNameExistsExcludeSelf(string name, int id)
        {
            return await _studentRepository.GetTableNoTracking()
                .AnyAsync(s => s.Name.ToLower() == name.ToLower() && s.StudID != id);
        }

        public async Task<bool> IsStudentIdExist(int id)
        {
            return await _studentRepository.GetTableNoTracking().AnyAsync(s => s.StudID == id);
            
        }

        public async Task<string> UpdateStudentAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);

            return "Success";
        }
    }
}
