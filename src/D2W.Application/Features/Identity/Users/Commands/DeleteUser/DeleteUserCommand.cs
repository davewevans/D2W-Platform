namespace D2W.Application.Features.Identity.Users.Commands.DeleteUser;

public class DeleteUserCommand : IRequest<Envelope<string>>
{
    #region Public Properties

    public string Id { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IUserUseCase _userUseCase;

        #endregion Private Fields

        #region Public Constructors

        public DeleteUserCommandHandler(IUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _userUseCase.DeleteUser(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}