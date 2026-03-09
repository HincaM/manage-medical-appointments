using MediatR;
using Persons.Application.Features.Patients.Create;
using Persons.Application.Features.Patients.Delete;
using Persons.Application.Features.Patients.GetAll;
using Persons.Application.Features.Patients.GetPatientByIdentification;
using Persons.Application.Features.Patients.Update;
using Persons.Domain.Common;
using Persons.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Persons.Api.Controllers
{
    [RoutePrefix("patients")]
    public class PatientsController:  ApiController
    {
        private readonly IMediator _mediator;

        public PatientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create")]
        public async Task<Result<bool>> Create(CreatePatientCommand command, CancellationToken cancellationToken)
            => await _mediator.Send(command, cancellationToken);

        [HttpGet]
        [Route("getAll")]
        public async Task<Result<List<Person>>> GetAll(CancellationToken cancellationToken)
            => await _mediator.Send(new GetAllPatientsQuery(), cancellationToken);

        [HttpGet]
        [Route("getByIdentification")]
        public async Task<Result<Person>> GetByIdentification(string identification, CancellationToken cancellationToken)
            => await _mediator.Send(new GetPatientByIdentificationQuery { Identification = identification }, cancellationToken);

        [HttpPut]
        [Route("update")]
        public async Task<Result<bool>> Update(UpdatePatientCommand command, CancellationToken cancellationToken)
            => await _mediator.Send(command, cancellationToken);
        
        [HttpDelete]
        [Route("delete")]
        public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
            => await _mediator.Send(new DeletePatientCommand { Id = id }, cancellationToken);
    }
}