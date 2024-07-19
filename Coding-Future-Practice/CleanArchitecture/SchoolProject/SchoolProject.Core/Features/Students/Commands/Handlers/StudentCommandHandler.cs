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
                 IRequestHandler<CreateStudentCommand , Response<string>> , 
                 IRequestHandler<UpdateStudentCommand , Response<string>>

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

       
        public async Task<Response<string>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            // Check if the student with the provided ID exists
            var student = await _studentService.GetStudentByIDAsync(request.StudID);
            if (student == null)
            {
                return NotFound<string>($"No Student Found with this ID {request.StudID}");
            }

            // Check if the new name already exists for another student
            bool isNameExists = await _studentService.IsNameExistsExcludeSelf(request.Name, request.StudID);
            if (isNameExists)
            {
                return BadRequest<string>("This name already exists for another student.");
            }

            // Map the request data to the student entity
            var mappedStudent = _mapper.Map<Student>(request);

            // Call the service to update the student
            var result = await _studentService.UpdateStudentAsync(mappedStudent);

            if (result != "Success")
            {
                return BadRequest<string>("Failed to update the student.");
            }

            return Created<string>($"Updated Successfully. ID {mappedStudent.StudID}");
        }

        #endregion


    }
}
