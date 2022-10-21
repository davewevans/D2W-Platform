namespace D2W.Application.Features.Identity.Roles.Commands.DeleteRole;

public class DeleteRoleCommand : IRequest<Envelope<string>>
{
    #region Public Properties

    public string Id { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IRoleUseCase _roleUseCase;

        #endregion Private Fields

        #region Public Constructors

        public DeleteRoleCommandHandler(IRoleUseCase roleUseCase)
        {
            _roleUseCase = roleUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            return await _roleUseCase.DeleteRole(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}