using MediatR;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
    public class StudentHandler :
                 IRequestHandler<GetStudentsListQuery, List<Student>>
    {
        private readonly IStudentService _studentService;

        public StudentHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<List<Student>> Handle(GetStudentsListQuery request,
                                                CancellationToken cancellationToken)
        {
            return await _studentService.GetAllStudentsAsync();
        }
    }
}
