using ErrorOr;
using MediatR;

namespace Authentication.Api.Application.Login;

public record LoginCommand(string UserName, string Password) : IRequest<ErrorOr<string>>;
