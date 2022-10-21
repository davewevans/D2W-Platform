namespace D2W.Application.Features.Identity.Manage.Commands.DeletePersonalData;

public class DeletePersonalDataCommandValidator : AbstractValidator<DeletePersonalDataCommand>
{
    #region Public Constructors

    public DeletePersonalDataCommandValidator()
    {
        RuleFor(v => v.Password).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resource.Password_is_required);
    }

    #endregion Public Constructors
}