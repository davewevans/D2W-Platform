namespace D2W.Application.Features.POC.Applicants.Commands.DeleteApplicant;

public class DeleteApplicantCommandValidator : AbstractValidator<DeleteApplicantCommand>
{
    #region Public Constructors

    public DeleteApplicantCommandValidator()
    {
        RuleFor(v => v.Id).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resource.Invalid_applicant_Id);
    }

    #endregion Public Constructors
}