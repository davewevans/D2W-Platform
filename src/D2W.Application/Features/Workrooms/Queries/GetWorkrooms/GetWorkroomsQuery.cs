using D2W.Application.Common.Interfaces.UseCases;

namespace D2W.Application.Features.Workrooms.Queries.GetWorkrooms;

public class GetWorkroomsQuery : FilterableQuery, IRequest<Envelope<WorkroomsResponse>>
{
    #region Public Classes

    public class GetWorkroomsQueryHandler : IRequestHandler<GetWorkroomsQuery, Envelope<WorkroomsResponse>>
    {
        #region Private Fields

        private readonly IWorkroomUseCase _workroomUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetWorkroomsQueryHandler(IWorkroomUseCase workroomUseCase)
        {
            _workroomUseCase = workroomUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<WorkroomsResponse>> Handle(GetWorkroomsQuery request, CancellationToken cancellationToken)
        {
            return await _workroomUseCase.GetWorkrooms(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}

