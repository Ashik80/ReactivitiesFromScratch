using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class Unattend
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor userAccessor;
            private readonly UserManager<AppUser> userManager;
            public Handler(DataContext context, IUserAccessor userAccessor, UserManager<AppUser> userManager)
            {
                this.userManager = userManager;
                this.userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);

                if (activity == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Activity = "Could not find activity" });
                }

                var user = await userManager.FindByNameAsync(userAccessor.GetCurrentUsername());

                var attendence = await _context.UserActivities
                    .FirstOrDefaultAsync(ua => ua.ActivityId == activity.Id && ua.AppUserId == user.Id);

                if (attendence == null)
                {
                    return Unit.Value;
                }

                if(attendence.IsHost)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new {Attendence = "You cannot remove yourself as host"});
                }

                _context.UserActivities.Remove(attendence);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;
                throw new Exception("Problem saving activity");
            }
        }
    }
}