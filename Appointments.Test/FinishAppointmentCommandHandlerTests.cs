using Appointments.Application.Features.Appointments.FinishAppointment;
using Appointments.Application.Features.Appointments.ScheduleAppointment;
using Appointments.Application.Features.Appointments.StartAppointment;
using Appointments.Application.Interfaces;
using Appointments.Domain.Entities;
using Appointments.Domain.Events;
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
    public class FinishAppointmentCommandHandlerTests
    {
        private FinishAppointmentCommandHandler _handler;
        private Mock<IAppointmentRepository> _appointmentRepository;
        private Mock<IEventBus> _eventBus;

        [SetUp]
        public void Setup()
        {
            _appointmentRepository = new Mock<IAppointmentRepository>();
            _eventBus = new Mock<IEventBus>();
            _handler = new FinishAppointmentCommandHandler(_appointmentRepository.Object, _eventBus.Object);
        }


        [Test]
        public async Task FinishSuccess()
        {
            // Arrange
            var command = new FinishAppointmentCommand
            {
                AppointmentId = 1,
                Description = "Acetamienofen 500mg tomar 1 cada 8 horas por 7 días",
                DoctorId = 1,
                PatientId = 1
            };

            _appointmentRepository
                .Setup(repo => repo.Finish(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _eventBus.Setup(bus => bus.Publish(It.IsAny<AppointmentFinishedEvent>()));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeTrue();
        }

        [Test]
        public async Task FinishFailureValidation()
        {
            // Arrange
            var command = new FinishAppointmentCommand
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
            result.Error.Should().NotBeNull();
        }

        [Test]
        public async Task FinishFailure()
        {
            // Arrange
            var command = new FinishAppointmentCommand
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
