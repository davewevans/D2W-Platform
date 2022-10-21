namespace D2W.WebPortal.Features.POC.Applicants.Commands.UpdateApplicant;

public class ReferenceItemForEditValidator : AbstractValidator<ReferenceItemForEdit>
{
    #region Public Constructors

    public ReferenceItemForEditValidator()
    {
        RuleFor(r => r.Name).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resource.Reference_name_is_required);

        RuleFor(r => r.Phone).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resource.Phone_number_is_required);
    }

    #endregion Public Constructors
}