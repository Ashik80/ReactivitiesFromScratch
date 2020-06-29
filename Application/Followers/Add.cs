using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Followers
{
    public class Add
    {
        public class Command : IRequest
        {
            public string UserName { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor userAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                this.userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var observer = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userAccessor.GetCurrentUsername());

                var target = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);

                if (target == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { user = "Not Found" });
                }

                var following = await _context.Followings.FirstOrDefaultAsync(f => f.ObserverId == observer.Id && f.TargetId == target.Id);

                if (following != null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { user = "Your are already following this user" });
                }
                else
                {
                    following = new UserFollowing
                    {
                        Observer = observer,
                        Target = target
                    };

                    _context.Followings.Add(following);
                }

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving activity");
            }
        }
    }
}