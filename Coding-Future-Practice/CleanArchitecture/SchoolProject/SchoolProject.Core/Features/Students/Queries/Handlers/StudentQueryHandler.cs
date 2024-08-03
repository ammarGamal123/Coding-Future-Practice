using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
    public class StudentQueryHandler(IStudentService studentService, IMapper mapper) :
                 ResponseHandler ,
                 IRequestHandler<GetStudentsListQuery, Response<List<GetStudentsListResponse>>> ,
                 IRequestHandler<GetStudentByIDQuery , Response<GetStudentByIDResponse>> , 
                 IRequestHandler<GetStudentPaginatedListQuery ,
                     PaginationResult<GetStudentPaginatedListResponse>>
    {
        private readonly IStudentService _studentService = studentService;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<GetStudentsListResponse>>> Handle
            (GetStudentsListQuery request,CancellationToken cancellationToken)
        {

            var students = await _studentService.GetAllStudentsAsync();
            var studentsMapper = _mapper.Map<List<GetStudentsListResponse>>(students);

            return Success(studentsMapper);
        }

        public async Task<Response<GetStudentByIDResponse>> Handle
            (GetStudentByIDQuery request, CancellationToken cancellationToken)
        {

            var student = await _studentService.GetStudentByIDAsync(request.Id);
            if (student == null)
                return NotFound<GetStudentByIDResponse>($"Student Not Found By this ID : {request.Id}");

            var studentMapper = _mapper.Map<GetStudentByIDResponse>(student);

            return Success(studentMapper);

        }

        public async Task<PaginationResult<GetStudentPaginatedListResponse>> Handle
            (GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            //Expression looks like mapping from student to GetStudentPaginatedListResponse

            //                       out = getstudent...resposne
            Expression<Func<Student, GetStudentPaginatedListResponse>>
                expression = stud => new GetStudentPaginatedListResponse
                                            (stud.StudID , stud.Name , stud.Address , 
                                            // in IQueryable Needs Department to be included
                                             stud.Department.Name);

           // var queryable = _studentService.GetStudentsQueryable();

            var filterQuery = _studentService.FilterStudentPaginatedQuery(request.OrderBy,
                                                                          request.Search);
            
            var paginatedList = await filterQuery.Select(expression)
                               .ToPaginationListAsync(request.PageNumber, request.PageSize);

            return paginatedList;
        }
    }
}
