using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.Fabrics.Commands.CreateFabric;

public class CreateFabricCommandValidator : AbstractValidator<CreateFabricCommand>
{
	#region Public Constructors

	public CreateFabricCommandValidator()
	{
		
	}    

    #endregion Public Constructors
}
