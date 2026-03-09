using FluentValidation;

namespace Authentication.Api.Application.Login
{
    public class LoginCommandValidation: AbstractValidator<LoginCommand>
    {
        public LoginCommandValidation() 
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("El nombre de usuario es requerido.")
                .MaximumLength(100).WithMessage("El nombre de usuario no puede tener más de 100 caracteres.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida.")
                .MaximumLength(100).WithMessage("La contraseña no puede tener más de 100 caracteres.");
        }
    }
}
