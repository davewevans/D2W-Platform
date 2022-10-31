using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.Identity.Account.Commands.RegisterWorkroom;

public class RegisterWorkroomCommandValidator : AbstractValidator<RegisterWorkroomCommand>
{
    #region Public Constructors

    public RegisterWorkroomCommandValidator()
    {
        RuleFor(v => v.Email).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resource.Username_is_required)
            .EmailAddress().WithMessage(v => string.Format(Resource.Username_is_invalid, v.Email))
            .MaximumLength(100).WithMessage(Resource.Username_must_not_exceed_200_characters)
            .MinimumLength(6).WithMessage(Resource.Username_must_be_at_least_6_characters);
    }

    #endregion Public Constructors
}
