using FluentAssertions;
using Moq;
using NUnit.Framework;
using Prescriptions.Application.Features.Prescriptions.GetById;
using Prescriptions.Domain.Entities;
using Prescriptions.Domain.Enums;
using Prescriptions.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Prescriptions.Tests
{
    [TestFixture]
    public class GetPrescriptionByIdQueryHandlerTests
    {
        private GetPrescriptionByIdQueryHandler _handler;
        private Mock<IPrescriptionsRepository> _prescriptionsRepository;

        [SetUp]
        public void Setup()
        {
            _prescriptionsRepository = new Mock<IPrescriptionsRepository>();
            _handler = new GetPrescriptionByIdQueryHandler(_prescriptionsRepository.Object);
        }


        [Test]
        public async Task GetSuccess()
        {
            // Arrange
            var command = new GetPrescriptionByIdQuery()
            {
                PrescriptionId = 1
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

            _prescriptionsRepository
                .Setup(repo => repo.Get(It.IsAny<Specification<Prescription>>(), It.IsAny<CancellationToken>()))
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
            var command = new GetPrescriptionByIdQuery();

            _prescriptionsRepository
                .Setup(repo => repo.Get(It.IsAny<Specification<Prescription>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Prescription)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
        }
    }
}
