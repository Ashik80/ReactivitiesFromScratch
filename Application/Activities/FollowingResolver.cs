using System.Linq;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class FollowingResolver : IValueResolver<UserActivity, AttendeeDto, bool>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor userAccessor;
        public FollowingResolver(DataContext context, IUserAccessor userAccessor)
        {
            this.userAccessor = userAccessor;
            _context = context;
        }

        public bool Resolve(UserActivity source, AttendeeDto destination, bool destMember, ResolutionContext context)
        {
            var currentUser = _context.Users
                .FirstOrDefaultAsync(x => x.UserName == userAccessor.GetCurrentUsername()).Result;

            if(currentUser.Followings.Any(x => x.TargetId == source.AppUser.Id))
                return true;
            
            return false;
        }
    }
}