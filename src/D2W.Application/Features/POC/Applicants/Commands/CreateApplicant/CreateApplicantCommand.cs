namespace D2W.Application.Features.POC.Applicants.Commands.CreateApplicant;

public class CreateApplicantCommand : IRequest<Envelope<CreateApplicantResponse>>
{
    #region Public Constructors

    public CreateApplicantCommand()
    {
        ReferenceItems = new List<ReferenceItemForAdd>();
    }

    #endregion Public Constructors

    #region Public Properties

    public int Ssn { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }

    public decimal Bmi
    {
        get => Height != 0 ? Weight / (Height / 100 * 2) : 0;
        set
        {
        }
    }

    public IList<ReferenceItemForAdd> ReferenceItems { get; set; }

    #endregion Public Properties

    #region Public Methods

    public Applicant MapToEntity()
    {
        return new()
        {
            Ssn = Ssn,
            FirstName = FirstName,
            LastName = LastName,
            DateOfBirth = DateOfBirth,
            Height = Height,
            Weight = Weight,
            References = ReferenceItems.Select(ri => new Reference
            {
                Name = ri.Name,
                JobTitle = ri.JobTitle,
                Phone = ri.Phone,
            }).ToList()
        };
    }

    #endregion Public Methods

    #region Public Classes

    public class CreateApplicantCommandHandler : IRequestHandler<CreateApplicantCommand, Envelope<CreateApplicantResponse>>
    {
        #region Private Fields

        private readonly IApplicantUseCase _applicantUseCase;

        #endregion Private Fields

        #region Public Constructors

        public CreateApplicantCommandHandler(IApplicantUseCase applicantUseCase)
        {
            _applicantUseCase = applicantUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<CreateApplicantResponse>> Handle(CreateApplicantCommand request, CancellationToken cancellationToken)
        {
            return await _applicantUseCase.AddApplicant(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}