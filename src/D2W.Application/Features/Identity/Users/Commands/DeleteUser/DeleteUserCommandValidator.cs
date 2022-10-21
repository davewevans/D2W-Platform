namespace D2W.Application.Features.Identity.Users.Commands.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    #region Public Constructors

    public DeleteUserCommandValidator()
    {
        RuleFor(v => v.Id).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resource.Invalid_user_Id);
    }

    #endregion Public Constructors
}