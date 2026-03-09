using FluentAssertions;
using Moq;
using NUnit.Framework;
using Persons.Application.Features.Patients.GetPatientByIdentification;
using Persons.Domain.Entities;
using Persons.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Tests.Patients
{
    [TestFixture]
    public class GetPatientByIdQueryHandlerTests
    {
        private GetPatientByIdentificationQueryHandler _handler;
        private Mock<IPatientsRepository> _patientsRepository;

        [SetUp]
        public void Setup()
        {
            _patientsRepository = new Mock<IPatientsRepository>();
            _handler = new GetPatientByIdentificationQueryHandler(_patientsRepository.Object);
        }


        [Test]
        public async Task GetSuccess()
        {
            // Arrange
            var command = new GetPatientByIdentificationQuery
            {
                Identification = "123456789",
            };
            var response = new Person 
            { 
                Id = 1, 
                LastName = "Hernandez", 
                Identification = "123456789", 
                FirstName = "John" 
            };

            _patientsRepository
                .Setup(repo => repo.Get(It.IsAny<PatientSpecificationBase>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }

        [Test]
        public async Task GetFailure()
        {
            // Arrange
            var command = new GetPatientByIdentificationQuery
            {
                Identification = "123456789",
            };

            _patientsRepository
                .Setup(repo => repo.Get(It.IsAny<PatientSpecificationBase>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Person)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeNull();
        }

        [Test]
        public async Task GetFailureValidation()
        {
            // Arrange
            var command = new GetPatientByIdentificationQuery
            {
            };

            _patientsRepository
                .Setup(repo => repo.Get(It.IsAny<PatientSpecificationBase>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Person)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Error.Should().NotBeNull();
        }
    }
}
