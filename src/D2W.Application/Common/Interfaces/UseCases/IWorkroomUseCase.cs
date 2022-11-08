using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2W.Application.Features.Workrooms.Queries.GetWorkroomForEdit;
using D2W.Application.Features.Workrooms.Queries.GetWorkrooms;
using D2W.Application.Features.Workrooms.Commands.UpdateWorkroom;
using D2W.Application.Features.Workrooms.Commands.DeleteWorkroom;


namespace D2W.Application.Common.Interfaces.UseCases;

public interface IWorkroomUseCase
{
    #region Public Methods

    Task<Envelope<WorkroomForEdit>> GetWorkroom(GetWorkroomForEditQuery request);
    Task<Envelope<WorkroomsResponse>> GetWorkrooms(GetWorkroomsQuery request);
    Task<Envelope<string>> EditWorkroom(UpdateWorkroomCommand request);
    Task<Envelope<string>> DeleteWorkroom(DeleteWorkroomCommand request);


    #endregion Public Methods
}
