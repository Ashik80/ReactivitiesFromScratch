using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.Photos
{
    public class Delete
    {
        public class Command : IRequest
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IUserAccessor userAccessor;
            private readonly IPhotoAccessor photoAccessor;
            private readonly UserManager<AppUser> userManager;
            private readonly DataContext context;
            public Handler(DataContext context, IUserAccessor userAccessor, IPhotoAccessor photoAccessor, UserManager<AppUser> userManager)
            {
                this.context = context;
                this.userManager = userManager;
                this.photoAccessor = photoAccessor;
                this.userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByNameAsync(userAccessor.GetCurrentUsername());

                var photo = user.Photos.FirstOrDefault(p => p.Id == request.Id);

                if (photo == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Photo = "Not Found" });
                }

                if (photo.IsMain)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { Photo = "You cannot delete your main photo" });
                }

                var result = photoAccessor.DeletePhoto(photo.Id);

                if(result == null)
                {
                    throw new Exception("Problem deleting photo");
                }

                context.Photos.Remove(photo);

                var success = await context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;
                throw new Exception("Problem saving activity");
            }
        }
    }
}