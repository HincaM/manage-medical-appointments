using MediatR;
using Persons.Application.Features.Doctors.Create;
using Persons.Application.Features.Doctors.Delete;
using Persons.Application.Features.Doctors.GetAll;
using Persons.Application.Features.Doctors.GetById;
using Persons.Application.Features.Doctors.Update;
using Persons.Domain.Common;
using Persons.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Persons.Api.Controllers
{
    [RoutePrefix("doctors")]
    public class DoctorsController:  ApiController
    {
        private readonly IMediator _mediator;

        public DoctorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create")]
        public async Task<Result<bool>> Create(CreateDoctorCommand command, CancellationToken cancellationToken)
            => await _mediator.Send(command, cancellationToken);

        [HttpGet]
        [Route("getAll")]
        public async Task<Result<List<Person>>> GetAll(CancellationToken cancellationToken)
            => await _mediator.Send(new GetAllDoctorsQuery(), cancellationToken);

        [HttpGet]
        [Route("getById")]
        public async Task<Result<Person>> GetById(int id, CancellationToken cancellationToken)
            => await _mediator.Send(new GetDoctorByIdQuery { Id = id }, cancellationToken);

        [HttpPut]
        [Route("update")]
        public async Task<Result<bool>> Update(UpdateDoctorCommand command, CancellationToken cancellationToken)
            => await _mediator.Send(command, cancellationToken);
        
        [HttpDelete]
        [Route("delete")]
        public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
            => await _mediator.Send(new DeleteDoctorCommand { Id = id }, cancellationToken);
    }
}