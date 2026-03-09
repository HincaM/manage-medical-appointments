using MediatR;
using Prescriptions.Application.Specifications;
using Prescriptions.Domain.Common;
using Prescriptions.Domain.Entities;
using Prescriptions.Domain.Interfaces;
using Prescriptions.Domain.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Prescriptions.Application.Features.Prescriptions.GetByPatientIdentification
{
    public class GetByPatientIdentificationQueryHandler : IRequestHandler<GetByPatientIdentificationQuery, Result<List<Prescription>>>
    {
        private readonly IHttpService _httpService;
        private readonly IPrescriptionsRepository _prescriptionsRepository;
        private readonly IUrlPersons _urlPersons;
        public GetByPatientIdentificationQueryHandler(IHttpService httpService, IPrescriptionsRepository prescriptionsRepository, IUrlPersons urlPersons)
        {
            _httpService = httpService;
            _prescriptionsRepository = prescriptionsRepository;
            _urlPersons = urlPersons;
        }
        public async Task<Result<List<Prescription>>> Handle(GetByPatientIdentificationQuery request, CancellationToken cancellationToken)
        {
            var response = await _httpService.Get<Result<PatientDto>>(_urlPersons.Value, $"patients/getByIdentification?identification={request.PatientIdentification}", cancellationToken);
            if (response.IsSuccess && response.Value != null)
            {
                var prescription = await _prescriptionsRepository.GetAll(new GetByPatientIdSpecification(response.Value.Id), cancellationToken);
                return Result<List<Prescription>>.Success(prescription);
            }
            else
            {
                return Result<List<Prescription>>.Failure("Recetas de paciente, no encontradas");
            }
        }
    }
}
