using Prescriptions.Application.Features.Prescriptions.GetByPatientIdentification;
using Prescriptions.Domain.Common;
using Prescriptions.Domain.Interfaces;
using Prescriptions.Domain.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Prescriptions.Infrastructure.Services
{
    public class PatientsService : IPatientsService
    {
        private readonly IHttpService _httpService;
        private readonly IConnectionString _connectionString;
        private readonly IPrescriptionsRepository _repository;

        public PatientsService(IHttpService httpService, IConnectionString connectionString)
        {
            _httpService = httpService;
            _connectionString = connectionString;
        }
        public async Task<int> GetIdByIdentification(string identification, CancellationToken cancellationToken)
        {
            var result = await _httpService.Get<Result<PatientDto>>(_connectionString.Value, $"api/patients/id?identification={identification}", cancellationToken);
            if (result.IsSuccess && result.Value != null)
            {

            }
            return 1;
        }
    }
}
