using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Followers;
using Application.Profiles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/profiles")]
    [ApiController]
    public class FollowersController : ControllerBase
    {
        private readonly IMediator mediator;
        public FollowersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("follow/{userName}")]
        public async Task<ActionResult<Unit>> Follow(string userName)
        {
            return await mediator.Send(new Add.Command { UserName = userName });
        }

        [HttpDelete("follow/{userName}")]
        public async Task<ActionResult<Unit>> UnFollow(string userName)
        {
            return await mediator.Send(new Delete.Command { UserName = userName });
        }

        [HttpGet("follow/{userName}")]
        public async Task<ActionResult<List<Profile>>> GetFollowing(string userName, string predicate)
        {
            return await mediator.Send(new List.Query { UserName = userName, Predicate = predicate });
        }
    }
}