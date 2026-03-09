using FluentAssertions;
using Moq;
using NUnit.Framework;
using Prescriptions.Application.Features.Prescriptions.GetByPatientIdentification;
using Prescriptions.Domain.Common;
using Prescriptions.Domain.Entities;
using Prescriptions.Domain.Enums;
using Prescriptions.Domain.Interfaces;
using Prescriptions.Domain.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Prescriptions.Tests
{
    [TestFixture]
    public class GetPrescriptionByPatientIdentificationQueryHandlerTests
    {
        private GetByPatientIdentificationQueryHandler _handler;
        private Mock<IPrescriptionsRepository> _prescriptionsRepository;
        private Mock<IHttpService> _httpService;
        private Mock<IUrlPersons> _urlPersons;

        [SetUp]
        public void Setup()
        {
            _prescriptionsRepository = new Mock<IPrescriptionsRepository>();
            _httpService = new Mock<IHttpService>();
            _urlPersons = new Mock<IUrlPersons>();
            _handler = new GetByPatientIdentificationQueryHandler(
                _httpService.Object,
                _prescriptionsRepository.Object,
                _urlPersons.Object
                );
        }


        [Test]
        public async Task GetSuccess()
        {
            // Arrange
            var command = new GetByPatientIdentificationQuery
            {
                PatientIdentification = "123456789",
            };
            var patient = new PatientDto
            {
                Id = 1,
                LastName = "Hernandez",
                Identification = "123456789",
                FirstName = "John"
            };
            var response = new Prescription
            {
                Id = 1,
                AppointmentId = 1,
                PatientId = 1,
                DoctorId = 1,
                Description = "Acetaminofen 500mg tomar 1 cada 8 horas por 7 días",
                Status = PrescriptionStatus.Active,
                CreatedAt = System.DateTime.UtcNow
            };

            _urlPersons.Setup(x => x.Value).Returns("http://testapi.com/patients");

            _httpService
                .Setup(x => x.Get<Result<PatientDto>>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<PatientDto>.Success(patient));

            _prescriptionsRepository
                .Setup(repo => repo.GetAll(It.IsAny<Specification<Prescription>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Prescription>() { response });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }

        [Test]
        public async Task GetFailureHttp()
        {
            // Arrange
            var command = new GetByPatientIdentificationQuery
            {
                PatientIdentification = "123456789",
            };

            _urlPersons.Setup(x => x.Value).Returns("http://testapi.com/patients");

            _httpService
                .Setup(x => x.Get<Result<PatientDto>>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<PatientDto>.Failure("Paciente no encontrado"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
        }

        [Test]
        public async Task GetSuccessNotFout()
        {
            // Arrange
            var command = new GetByPatientIdentificationQuery
            {
                PatientIdentification = "123456789",
            };
            var patient = new PatientDto
            {
                Id = 1,
                LastName = "Hernandez",
                Identification = "123456789",
                FirstName = "John"
            };

            _urlPersons.Setup(x => x.Value).Returns("http://testapi.com/patients");

            _httpService
                .Setup(x => x.Get<Result<PatientDto>>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<PatientDto>.Success(patient));

            _prescriptionsRepository
                .Setup(repo => repo.GetAll(It.IsAny<Specification<Prescription>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Prescription>());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().HaveCount(0);
            result.Error.Should().BeNull();
        }
    }
}
