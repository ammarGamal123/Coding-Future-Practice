using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler :
                 ResponseHandler,
                 IRequestHandler<CreateStudentCommand, Response<string>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors

        public StudentCommandHandler(IStudentService studentService, IMapper mapper = null)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        #endregion

        #region Handle Functions

        public async Task<Response<string>> Handle(CreateStudentCommand request,
                                             CancellationToken cancellationToken)
        {
            // Mapping from request to student
            var student = _mapper.Map<Student>(request);
            // Add
            var result = await _studentService.CreateStudentAsync(student);

            // Check the response coming from _studentService
            if (result == "Name is already exists")
                return UnprocessableEntity<string>("Name is already exists");

            // return response
            else if (result == "Added Successfully")
                return Created<string>("Added Successfully");
            else
                return BadRequest<string>("something wrong happened");
        }
        #endregion
        
    }
}
