using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System;
using Domain.Auth;
using ISession = Domain.Auth.ISession;

namespace Application.Auth
{
    public class Session: ISession
    {
        public Guid UserId { get; private set; }

        public DateTime Now => DateTime.Now;

        public Session(IHttpContextAccessor httpContextAccessor)
        {
            var user  = httpContextAccessor.HttpContext?.User;
            var nameIdentifier = user?.FindFirst(ClaimTypes.NameIdentifier);
            if (nameIdentifier != null)
            {
                UserId = new Guid(nameIdentifier.Value);
            }
        }
    }
}
