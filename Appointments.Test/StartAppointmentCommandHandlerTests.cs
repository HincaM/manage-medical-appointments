using Appointments.Application.Features.Appointments.ScheduleAppointment;
using Appointments.Application.Features.Appointments.StartAppointment;
using Appointments.Domain.Entities;
using Appointments.Domain.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Appointments.Test
{
    [TestFixture]
    public class StartAppointmentCommandHandlerTests
    {
        private StartAppointmentCommandHandler _handler;
        private Mock<IAppointmentRepository> _appointmentRepository;

        [SetUp]
        public void Setup()
        {
            _appointmentRepository = new Mock<IAppointmentRepository>();
            _handler = new StartAppointmentCommandHandler(_appointmentRepository.Object);
        }


        [Test]
        public async Task StartSuccess()
        {
            // Arrange
            var command = new StartAppointmentCommand
            {
                AppointmentId = 1
            };

            _appointmentRepository
                .Setup(repo => repo.Start(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeTrue();
        }

        [Test]
        public async Task StartFailure()
        {
            // Arrange
            var command = new StartAppointmentCommand
            {
                AppointmentId = 1
            };

            _appointmentRepository
                .Setup(repo => repo.Start(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeFalse();
        }
    }
}
