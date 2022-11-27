using D2W.Application.Common.Managers;
using D2W.Application.Features.Clients.Queries.GetClients;

namespace D2W.Application.Features.DesignConcepts.Queries.GetDesignConcepts;

public class DesignConceptDto : AuditableDto
{
    #region Public Constructors

    public DesignConceptDto()
    {
        WindowMeasurements = new WindowMeasurementsDto();
        DraperyCalculations = new DraperyCalculationsDto();
        WorkOrder = new WorkOrderDto();
    }

    #endregion Public Constructors


    #region Public Properties

    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Name { get; set; }

    public string ImageUrl { get; set; }

    public string ClientId { get; set; }

    public ClientItem Client { get; set; }

    public bool IsArchived { get; set; }

    public bool ApprovedByClient { get; set; }

    public string ClientNotes { get; set; }

    public WindowMeasurementsDto WindowMeasurements { get; set; }
    public DraperyCalculationsDto DraperyCalculations { get; set; }
    public WorkOrderDto WorkOrder { get; set; }

    #endregion Public Properties

    #region Public Methods

    public static DesignConceptDto MapFromEntity(DesignConceptModel designConcept)
    {
        return new()
        {
            Id = designConcept.Id,
            TenantId = designConcept.TenantId,
            Name = designConcept.Name,
            ImageUrl = designConcept.ImageUri,
            ClientId = designConcept.ClientId,
            IsArchived = designConcept.IsArchived,
            ApprovedByClient = designConcept.ApprovedByClient,
            ClientNotes = designConcept.ClientNotes,

            CreatedOn = designConcept.CreatedOn,
            CreatedBy = designConcept.CreatedBy,
            ModifiedOn = designConcept.ModifiedOn,
            ModifiedBy = designConcept.ModifiedBy,

            Client = ClientItem.MapFromEntity(designConcept.Client, designConcept.TenantId),
            WindowMeasurements = WindowMeasurementsDto.MapFromEntity(designConcept.WindowMeasurements),
            DraperyCalculations = DraperyCalculationsDto.MapFromEntity(designConcept.DraperyCalculations)
        };
    }

    #endregion Public Methods
}
