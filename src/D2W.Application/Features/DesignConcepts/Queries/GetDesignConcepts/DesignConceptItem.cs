using D2W.Application.Features.Clients.Queries.GetClients;

namespace D2W.Application.Features.DesignConcepts.Queries.GetDesignConcepts;

public class DesignConceptItem : AuditableDto
{
    #region Public Constructors

    public DesignConceptItem()
    {
        FabricCalculationsItems = new List<FabricCalculationsItem>();
    }

    #endregion Public Constructors


    #region Public Properties

    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Name { get; set; }

    public string ImageUrl { get; set; }

    public Guid? ClientId { get; set; }

    public ClientItem Client { get; set; }

    public bool IsArchived { get; set; }

    public bool ApprovedByClient { get; set; }

    public string ClientNotes { get; set; }

    public WindowMeasurementsItem WindowMeasurementsItem { get; set; }
    public List<FabricCalculationsItem> FabricCalculationsItems { get; set; }

    #endregion Public Properties

    #region Public Methods

    public static DesignConceptItem MapFromEntity(DesignConceptModel designConcept)
    {
        return new()
        {
            Id = designConcept.Id,
            TenantId = designConcept.TenantId,
            Name = designConcept.Name,
            ImageUrl = designConcept.ImageUrl,
            ClientId = designConcept.ClientId,
            IsArchived = designConcept.IsArchived,
            ApprovedByClient = designConcept.ApprovedByClient,
            ClientNotes = designConcept.ClientNotes,

            CreatedOn = designConcept.CreatedOn,
            CreatedBy = designConcept.CreatedBy,
            ModifiedOn = designConcept.ModifiedOn,
            ModifiedBy = designConcept.ModifiedBy,

            Client = ClientItem.MapFromEntity(designConcept.ApplicationUser, designConcept.TenantId),
            WindowMeasurementsItem = WindowMeasurementsItem.MapFromEntity(designConcept.WindowMeasurements),
            FabricCalculationsItems = designConcept.FabricCalculations?.Select(fc => FabricCalculationsItem.MapFromEntity(fc)).ToList()
        };
    }

    #endregion Public Methods
}
