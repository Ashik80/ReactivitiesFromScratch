using System.Threading.Tasks;
using Application.Photos;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IMediator mediator;
        public PhotosController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Photo>> Add([FromForm]Add.Command command)
        {
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(string id)
        {
            return await mediator.Send(new Delete.Command{Id = id});
        }

        [HttpPut("setmain/{id}")]
        public async Task<ActionResult<Unit>> SetMain(string id)
        {
            return await mediator.Send(new SetMain.Command{Id = id});
        }
    }
}