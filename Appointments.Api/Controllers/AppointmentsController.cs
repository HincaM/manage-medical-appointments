using Appointments.Application.Features.Appointments.FinishAppointment;
using Appointments.Application.Features.Appointments.ScheduleAppointment;
using Appointments.Application.Features.Appointments.StartAppointment;
using Appointments.Domain.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Appointments.Api.Controllers
{

    [RoutePrefix("appointments")]
    public  class AppointmentsController: ApiController
    {
        public readonly IMediator _mediator;
        public AppointmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("schedule")]
        public async Task<Result<bool>> ScheduleAppointment(ScheduleAppointmentCommand command, CancellationToken cancellationToken) 
            => await _mediator.Send(command, cancellationToken);

        [HttpPut]
        [Route("start")]
        public async Task<Result<bool>> StartAppointment(StartAppointmentCommand command, CancellationToken cancellationToken)
            => await _mediator.Send(command, cancellationToken);

        [HttpPut]
        [Route("finish")]
        public async Task<Result<bool>> FinishAppointment(FinishAppointmentCommand command, CancellationToken cancellationToken)
            => await _mediator.Send(command, cancellationToken);
    }
}
