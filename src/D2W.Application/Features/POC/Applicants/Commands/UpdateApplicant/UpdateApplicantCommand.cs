namespace D2W.Application.Features.POC.Applicants.Commands.UpdateApplicant;

public class UpdateApplicantCommand : IRequest<Envelope<string>>
{
    #region Public Constructors

    public UpdateApplicantCommand()
    {
        NewApplicantReferences = new List<ReferenceItemForAdd>();
        ModifiedApplicantReferences = new List<ReferenceItemForEdit>();
        RemovedApplicantReferences = new List<string>();
    }

    #endregion Public Constructors

    #region Public Properties

    public string Id { get; set; }
    public int Ssn { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }

    public decimal Bmi
    {
        get => Height != 0 ? Weight / (Height / 100 * 2) : 0;
        set { if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value)); }
    }

    public List<ReferenceItemForAdd> NewApplicantReferences { get; set; }
    public List<ReferenceItemForEdit> ModifiedApplicantReferences { get; set; }
    public List<string> RemovedApplicantReferences { get; set; }

    #endregion Public Properties

    #region Public Methods

    public async Task MapToEntity(Applicant applicant, IApplicationDbContext dbContext)
    {
        if (applicant == null)
            throw new ArgumentNullException(nameof(applicant));

        applicant.Ssn = Ssn;
        applicant.FirstName = FirstName;
        applicant.LastName = LastName;
        applicant.DateOfBirth = DateOfBirth;
        applicant.Height = Height;
        applicant.Weight = Weight;

        await UpdateApplicantReferences(applicant, dbContext);
    }

    #endregion Public Methods

    #region Private Methods

    private async Task UpdateApplicantReferences(Applicant applicant, IApplicationDbContext dbContext)
    {
        AddReferences(applicant);

        await ModifyReferences(dbContext);

        await RemoveReferences(applicant, dbContext);
    }

    private async Task RemoveReferences(Applicant applicant, IApplicationDbContext dbContext)
    {
        foreach (var referenceId in RemovedApplicantReferences)
        {
            var referenceFromDb = await dbContext.References.FindAsync(Guid.Parse(referenceId));
            applicant.References.Remove(referenceFromDb);
        }
    }

    private async Task ModifyReferences(IApplicationDbContext dbContext)
    {
        foreach (var reference in ModifiedApplicantReferences)
        {
            var referenceFromDb = await dbContext.References.FindAsync(Guid.Parse(reference.Id));

            if (referenceFromDb != null)
            {
                referenceFromDb.Name = reference.Name;
                referenceFromDb.JobTitle = reference.JobTitle;
                referenceFromDb.Phone = reference.Phone;
            }
        }

        ;
    }

    private void AddReferences(Applicant applicant)
    {
        NewApplicantReferences?.ForEach(r => applicant.References.Add(new Reference
        {
            Name = r.Name,
            JobTitle = r.JobTitle,
            Phone = r.Phone
        }));
    }

    #endregion Private Methods

    #region Public Classes

    public class UpdateApplicantCommandHandler : IRequestHandler<UpdateApplicantCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IApplicantUseCase _applicantUseCase;

        #endregion Private Fields

        #region Public Constructors

        public UpdateApplicantCommandHandler(IApplicantUseCase applicantUseCase)
        {
            _applicantUseCase = applicantUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(UpdateApplicantCommand request, CancellationToken cancellationToken)
        {
            return await _applicantUseCase.EditApplicant(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}