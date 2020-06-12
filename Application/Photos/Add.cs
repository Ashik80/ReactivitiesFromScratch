using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.Photos
{
    public class Add
    {
        public class Command : IRequest<Photo>
        {
            public IFormFile File { get; set; }
        }

        public class Handler : IRequestHandler<Command, Photo>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor userAccessor;
            private readonly IPhotoAccessor photoAccessor;
            private readonly UserManager<AppUser> userManager;
            public Handler(DataContext context, UserManager<AppUser> userManager, IUserAccessor userAccessor, IPhotoAccessor photoAccessor)
            {
                this.userManager = userManager;
                this.photoAccessor = photoAccessor;
                this.userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Photo> Handle(Command request, CancellationToken cancellationToken)
            {
                var photoUploadResult = photoAccessor.AddPhoto(request.File);

                var user = await userManager.FindByNameAsync(userAccessor.GetCurrentUsername());

                var photo = new Photo
                {
                    Url = photoUploadResult.Url,
                    Id = photoUploadResult.PublicId
                };

                if(!user.Photos.Any())
                {
                    photo.IsMain = true;
                }

                user.Photos.Add(photo);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return photo;
                
                throw new Exception("Problem saving activity");
            }
        }
    }
}