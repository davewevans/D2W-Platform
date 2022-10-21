namespace D2W.WebPortal.Features.POC.Applicants.Queries.ExportApplicants
{
    public class ExportApplicantsQuery
    {
        #region Public Properties

        public string SearchText { get; set; }
        public string SortBy { get; set; }
        public bool IsImmediate { get; set; } = true;

        #endregion Public Properties
    }
}