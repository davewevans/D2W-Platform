namespace D2W.WebPortal.Features.Identity.Manage.Commands.DeletePersonalData;

public class DeletePersonalDataCommandValidator : AbstractValidator<DeletePersonalDataCommand>
{
    #region Public Constructors

    public DeletePersonalDataCommandValidator()
    {
        RuleFor(v => v.Password).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(BackendResources.Resource.Password_is_required);
    }

    #endregion Public Constructors
}