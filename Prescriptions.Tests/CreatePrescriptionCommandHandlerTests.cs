using FluentAssertions;
using Moq;
using NUnit.Framework;
using Prescriptions.Application.Features.Prescriptions.Create;
using Prescriptions.Domain.Entities;
using Prescriptions.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Prescriptions.Tests
{
    [TestFixture]
    public class CreatePrescriptionCommandHandlerTests
    {
        private CreatePrescriptionCommandHandler _handler;
        private Mock<IPrescriptionsRepository> _prescriptionsRepository;

        [SetUp]
        public void Setup()
        {
            _prescriptionsRepository = new Mock<IPrescriptionsRepository>();
            _handler = new CreatePrescriptionCommandHandler(_prescriptionsRepository.Object);
        }


        [Test]
        public async Task CreateSuccess()
        {
            // Arrange
            var command = new CreatePrescriptionCommand
            {
                AppointmentId = 1,
                DoctorId = 1,
                PatientId = 1,
                Description = "Acetaminofen 500mg tomar 1 cada 8 horas por 7 días"
            };

            _prescriptionsRepository
                .Setup(repo => repo.Create(It.IsAny<Prescription>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeTrue();
        }

        [Test]
        public async Task CreateFailure()
        {
            // Arrange
            var command = new CreatePrescriptionCommand
            {
                AppointmentId = 1,
                DoctorId = 1,
                PatientId = 1,
                Description = "Acetaminofen 500mg tomar 1 cada 8 horas por 7 días"
            };

            _prescriptionsRepository
                .Setup(repo => repo.Create(It.IsAny<Prescription>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeFalse();
        }
    }
}



