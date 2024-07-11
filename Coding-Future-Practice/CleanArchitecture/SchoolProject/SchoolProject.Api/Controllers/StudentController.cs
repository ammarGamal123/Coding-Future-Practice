using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Router.StudentRouting.List)]
        public async Task<IActionResult> GetAllStudents()
        {
            return Ok(await _mediator.Send(new GetStudentsListQuery()));
        }

        [HttpGet(Router.StudentRouting.GetByID)]
        public async Task<IActionResult> GetStudentByIDAsync([FromRoute]int id)
        {
            return Ok(await _mediator.Send(new GetStudentByIDQuery(id)));
        }

        [HttpPost(Router.StudentRouting.Create)]
        public async Task<IActionResult> CreateStudentAsync
            ([FromBody] CreateStudentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

    }
}
