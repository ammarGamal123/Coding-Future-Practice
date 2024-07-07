using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.Students.Queries.Models;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Student/List")]
        public async Task<IActionResult> GetAllStudents()
        {
            return Ok(await _mediator.Send(new GetStudentsListQuery()));
        }

        [HttpGet("Student/{id}")]
        public async Task<IActionResult> GetStudentByIDAsync([FromRoute]int id)
        {
            return Ok(await _mediator.Send(new GetStudentByIDQuery(id)));
        }
    }
}
