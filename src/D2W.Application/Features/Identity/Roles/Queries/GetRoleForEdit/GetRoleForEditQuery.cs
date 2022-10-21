namespace D2W.Application.Features.Identity.Roles.Queries.GetRoleForEdit;

public class GetRoleForEditQuery : IRequest<Envelope<RoleForEdit>>
{
    #region Public Properties

    public string Id { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class GetRoleForEditQueryHandler : IRequestHandler<GetRoleForEditQuery, Envelope<RoleForEdit>>
    {
        #region Private Fields

        private readonly IRoleUseCase _roleUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetRoleForEditQueryHandler(IRoleUseCase roleUseCase)
        {
            _roleUseCase = roleUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<RoleForEdit>> Handle(GetRoleForEditQuery request, CancellationToken cancellationToken)
        {
            return await _roleUseCase.GetRole(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}