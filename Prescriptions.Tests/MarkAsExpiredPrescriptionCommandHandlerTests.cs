using FluentAssertions;
using Moq;
using NUnit.Framework;
using Prescriptions.Application.Features.Prescriptions.MarkAsExpired;
using Prescriptions.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Prescriptions.Tests
{
    public class MarkAsExpiredPrescriptionCommandHandlerTests
    {
        private MarkAsExpiredPrescriptionCommandHandler _handler;
        private Mock<IPrescriptionsRepository> _prescriptionsRepository;

        [SetUp]
        public void Setup()
        {
            _prescriptionsRepository = new Mock<IPrescriptionsRepository>();
            _handler = new MarkAsExpiredPrescriptionCommandHandler(_prescriptionsRepository.Object);
        }


        [Test]
        public async Task MarkAsExpiredSuccess()
        {
            // Arrange
            var command = new MarkAsExpiredPrescriptionCommand
            {
                PrescriptionId = 1
            };

            _prescriptionsRepository
                .Setup(repo => repo.MarkAsExpired(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeTrue();
        }

        [Test]
        public async Task MarkAsExpiredFailure()
        {
            // Arrange
            var command = new MarkAsExpiredPrescriptionCommand
            {
                PrescriptionId = 1
            };

            _prescriptionsRepository
                .Setup(repo => repo.MarkAsExpired(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeFalse();
        }
    }
}
