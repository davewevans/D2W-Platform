namespace D2W.Application.Features.POC.Applicants.Commands.DeleteApplicant;

public class DeleteApplicantCommand : IRequest<Envelope<string>>
{
    #region Public Properties

    public string Id { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class DeleteApplicantCommandHandler : IRequestHandler<DeleteApplicantCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IApplicantUseCase _applicantUseCase;

        #endregion Private Fields

        #region Public Constructors

        public DeleteApplicantCommandHandler(IApplicantUseCase applicantUseCase)
        {
            _applicantUseCase = applicantUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(DeleteApplicantCommand request, CancellationToken cancellationToken)
        {
            return await _applicantUseCase.DeleteApplicant(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}