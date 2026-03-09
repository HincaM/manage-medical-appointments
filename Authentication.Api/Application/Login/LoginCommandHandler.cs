using Authentication.Api.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace Authentication.Api.Application.Login
{
    public class LoginCommandHandler(ITokenService _tokenService) : IRequestHandler<LoginCommand, ErrorOr<string>>
    {
        public async Task<ErrorOr<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (request.UserName != "admin" || request.Password != "1234")
                return Error.Unauthorized("Usuario y/o contraseña inválida.");


            return _tokenService.GetToken(request.UserName, request.Password);
        }
    }
}
