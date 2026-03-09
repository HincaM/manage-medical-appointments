using MediatR;
using Prescriptions.Application.Features.Prescriptions.Create;
using Prescriptions.Application.Features.Prescriptions.Deliver;
using Prescriptions.Application.Features.Prescriptions.GetAll;
using Prescriptions.Application.Features.Prescriptions.GetById;
using Prescriptions.Application.Features.Prescriptions.GetByPatientIdentification;
using Prescriptions.Application.Features.Prescriptions.MarkAsExpired;
using Prescriptions.Domain.Common;
using Prescriptions.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Prescriptions.Api.Controllers
{
    [RoutePrefix("prescriptions")]
    public class PrescriptionsController: ApiController
    {
        private readonly IMediator _mediator;

        public PrescriptionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create")]
        public async Task<Result<bool>> Create(CreatePrescriptionCommand command, CancellationToken cancellationToken)
            => await _mediator.Send(command, cancellationToken);

        [HttpGet]
        [Route("getById")]
        public async Task<Result<Prescription>> GetById(int prescriptionId, CancellationToken cancellationToken)
            => await _mediator.Send(new GetPrescriptionByIdQuery { PrescriptionId = prescriptionId }, cancellationToken);

        [HttpGet]
        [Route("getByPatientIdentification")]
        public async Task<Result<List<Prescription>>> GetByPatientIdentification(string patientIdentification, CancellationToken cancellationToken)
            => await _mediator.Send(new GetByPatientIdentificationQuery { PatientIdentification = patientIdentification }, cancellationToken);

        [HttpGet]
        [Route("getAll")]
        public async Task<Result<List<Prescription>>> GetAll(CancellationToken cancellationToken)
            => await _mediator.Send(new GetAllPrescriptionsQuery(), cancellationToken);

        [HttpPut]
        [Route("deliver")]
        public async Task<Result<bool>> Deliver(DeliverPrescriptionCommand command, CancellationToken cancellationToken)
            => await _mediator.Send(command, cancellationToken);

        [HttpPut]
        [Route("markAsExpired")]
        public async Task<Result<bool>> MarkAsExpired(MarkAsExpiredPrescriptionCommand command, CancellationToken cancellationToken)
            => await _mediator.Send(command, cancellationToken);

    }
}
