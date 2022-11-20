using D2W.Application.Common.Interfaces.UseCases;
using D2W.Application.Features.Workrooms.Commands.DeleteWorkroom;

namespace D2W.Application.Features.Workrooms.Commands.DeleteWorkroom;

public class DeleteWorkroomCommand : IRequest<Envelope<string>>
{
    #region Public Properties

    public string Id { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class DeleteWorkroomCommandHandler : IRequestHandler<DeleteWorkroomCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IWorkroomUseCase _workroomUseCase;

        #endregion Private Fields

        #region Public Constructors

        public DeleteWorkroomCommandHandler(IWorkroomUseCase workroomUseCase)
        {
            _workroomUseCase = workroomUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(DeleteWorkroomCommand request, CancellationToken cancellationToken)
        {
            return await _workroomUseCase.DeleteWorkroom(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
