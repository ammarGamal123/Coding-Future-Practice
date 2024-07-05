using AutoMapper;
using MediatR;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
    public class StudentHandler(IStudentService studentService, IMapper mapper) :
                 IRequestHandler<GetStudentsListQuery, List<GetStudentsListResponse>>
    {
        private readonly IStudentService _studentService = studentService;
        private readonly IMapper _mapper = mapper;

        public async Task<List<GetStudentsListResponse>> Handle
            (GetStudentsListQuery request,CancellationToken cancellationToken)
        {
            var students = await _studentService.GetAllStudentsAsync();
            var studentsMapper = _mapper.Map<List<GetStudentsListResponse>>(students);

            return studentsMapper;
        }
    }
}
