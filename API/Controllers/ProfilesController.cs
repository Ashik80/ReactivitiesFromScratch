using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Profiles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProfilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<Profile>> Get(string userName)
        {
            return await _mediator.Send(new Details.Query { UserName = userName });
        }

        [HttpPut("{userName}")]
        public async Task<ActionResult<Unit>> Edit(string userName, Edit.Command command)
        {
            command.UserName = userName;
            return await _mediator.Send(command);
        }

        [HttpGet("activities/{userName}")]
        public async Task<ActionResult<List<UserActivityDto>>> GetUserActivities(string userName,
        string predicate)
        {
            return await _mediator
                .Send(new ListActivities.Query { UserName = userName, Predicate = predicate });
        }
    }
}