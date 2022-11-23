using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.Fabrics.Commands.DeleteFabric;

public class DeleteFabricCommandValidator : AbstractValidator<DeleteFabricCommand>
{
    #region Public Constructors

    public DeleteFabricCommandValidator()
    {
        //RuleFor(v => v.Id).Cascade(CascadeMode.Stop)
        //    .NotEmpty().WithMessage(Resource.Invalid_applicant_Id);
    }

    #endregion Public Constructors
}
