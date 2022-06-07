using App.Application.Student.Examination.SearchStudentSchool;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Clean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        public ProfileController(IMediator mediator)
        {
            Mediator = mediator;
        }

        public IMediator Mediator { get; }

        [HttpPost("studentSearch")]
        public async Task<IActionResult> Query([FromBody] SearchStudentSchoolQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpGet("hi")]
        public IActionResult Hi()
        {
            return Ok("Hi ...");
        }
    }
}
