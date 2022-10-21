namespace D2W.Application.Features.Identity.Roles.Queries.GetRoles;

public class GetRolesQuery : FilterableQuery, IRequest<Envelope<RolesResponse>>
{
    #region Public Classes

    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, Envelope<RolesResponse>>
    {
        #region Private Fields

        private readonly IRoleUseCase _roleUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetRolesQueryHandler(IRoleUseCase roleUseCase)
        {
            _roleUseCase = roleUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<RolesResponse>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return await _roleUseCase.GetRoles(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}