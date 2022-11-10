namespace D2W.Application.Features.DesignConcepts.Queries.GetDesignConceptForEdit;

public class DesignConceptForEdit : AuditableDto
{
    #region Public Properties


    #endregion Public Properties

    #region Public Methods

    public static DesignConceptForEdit MapFromEntity(DesignConceptModel designConcept)
    {
        return new()
        {
            


            //CreatedOn = appUser.CreatedOn,
            //CreatedBy = appUser.CreatedBy,
            //ModifiedOn = appUser.ModifiedOn,
            //ModifiedBy = appUser.ModifiedBy
        };
    }

    #endregion Public Methods
}
