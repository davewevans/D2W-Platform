using D2W.Application.Common.Interfaces.UseCases;

namespace D2W.Application.Features.Workrooms.Queries.GetWorkroomForEdit;

public class GetWorkroomForEditQuery : IRequest<Envelope<WorkroomForEdit>>
{
    #region Public Properties

    public string Id { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class GetWorkroomForEditQueryHandler : IRequestHandler<GetWorkroomForEditQuery, Envelope<WorkroomForEdit>>
    {
        #region Private Fields

        private readonly IWorkroomUseCase _workroomUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetWorkroomForEditQueryHandler(IWorkroomUseCase workroomUseCase)
        {
            _workroomUseCase = workroomUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<WorkroomForEdit>> Handle(GetWorkroomForEditQuery request, CancellationToken cancellationToken)
        {
            return await _workroomUseCase.GetWorkroom(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
