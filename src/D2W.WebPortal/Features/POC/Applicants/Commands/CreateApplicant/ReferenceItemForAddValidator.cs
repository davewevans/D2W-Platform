namespace D2W.WebPortal.Features.POC.Applicants.Commands.CreateApplicant;

public class ReferenceItemForAddValidator : AbstractValidator<ReferenceItemForAdd>
{
    #region Public Constructors

    public ReferenceItemForAddValidator()
    {
        RuleFor(r => r.Name).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resource.Reference_name_is_required);

        RuleFor(r => r.Phone).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resource.Phone_number_is_required);
    }

    #endregion Public Constructors
}