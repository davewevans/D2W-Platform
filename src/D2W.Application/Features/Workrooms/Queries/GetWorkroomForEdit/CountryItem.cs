using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.Workrooms.Queries.GetWorkroomForEdit;

public class CountryItem
{
    public Guid Id { get; set; }
    public string CountryName { get; set; }
    public string CountryCode { get; set; }
    public string CountryFlagUri { get; set; }
}
