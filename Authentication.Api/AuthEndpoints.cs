using Authentication.Api.Application.Login;
using MediatR;

namespace Authentication.Api
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            app.MapPost("auth/login", async (LoginCommand request, IMediator mediator, CancellationToken cancellation) =>
            {
                var result = await mediator.Send(request,cancellation);
                return result.Match(Results.Ok,error => Results.Unauthorized());
            });
        }
    }
}
