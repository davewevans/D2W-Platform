using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Features.DesignConcepts.Commands.CreateDesignConceptCommand
{
    public class CreateDesignConceptCommand
    {
        #region Public Constructors

        public CreateDesignConceptCommand()
        {
            DraperyCalculationsItem = new DraperyCalculationsItemForAdd();
            WindowMeasurementsItem = new WindowMeasurementsItemForAdd();
        }

        #endregion Public Constructors

        #region Public Properties

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string? ClientId { get; set; }

        public WindowMeasurementsItemForAdd WindowMeasurementsItem { get; set; }
        public DraperyCalculationsItemForAdd DraperyCalculationsItem { get; set; }

        #endregion Public Properties
    }
}