using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles
{
    public class ProfileReader : IProfileReader
    {
        private readonly IUserAccessor userAccessor;
        private readonly DataContext context;
        public ProfileReader(DataContext context, IUserAccessor userAccessor)
        {
            this.context = context;
            this.userAccessor = userAccessor;
        }

        public async Task<Profile> ReadProfile(string userName)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { user = "Not Found" });
            }

            var currentUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == userAccessor.GetCurrentUsername());

            var profile = new Profile
            {
                DisplayName = user.DisplayName,
                UserName = user.UserName,
                Image = user.Photos.FirstOrDefault(u => u.IsMain)?.Url,
                Bio = user.Bio,
                Photos = user.Photos,
                FollowersCount = user.Followers.Count(),
                FollowingCount = user.Followings.Count()
            };

            if(currentUser.Followings.Any(x => x.TargetId == user.Id))
            {
                profile.IsFollowed = true;
            }

            return profile;
        }
    }
}