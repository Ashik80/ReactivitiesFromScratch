using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.Profiles
{
    public class Details
    {
        public class Query : IRequest<Profile>
        {
            public string UserName { get; set; }
        }

        public class Handler : IRequestHandler<Query, Profile>
        {
            private readonly UserManager<AppUser> userManager;
            public Handler(DataContext context, UserManager<AppUser> userManager)
            {
                this.userManager = userManager;
            }
            public async Task<Profile> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByNameAsync(request.UserName);

                return new Profile
                {
                    DisplayName = user.DisplayName,
                    UserName = user.UserName,
                    Image = user.Photos.FirstOrDefault(u => u.IsMain)?.Url,
                    Bio = user.Bio,
                    Photos = user.Photos
                };
            }
        }
    }
}