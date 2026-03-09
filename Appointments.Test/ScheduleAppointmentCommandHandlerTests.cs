using Appointments.Application.Features.Appointments.ScheduleAppointment;
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
    public class ScheduleAppointmentCommandHandlerTests
    {
        private ScheduleAppointmentCommandHandler _handler;
        private Mock<IAppointmentRepository> _appointmentRepository;

        [SetUp]
        public void Setup()
        {
            _appointmentRepository = new Mock<IAppointmentRepository>();
            _handler = new ScheduleAppointmentCommandHandler(_appointmentRepository.Object);
        }


        [Test]
        public async Task ScheduleSuccess()
        {
            // Arrange
            var command = new ScheduleAppointmentCommand
            {
                PatientId = 1,
                DoctorId = 1,
                Date = DateTime.Now.AddDays(1),
                Location = "Sala 101"
            };
            _appointmentRepository
                .Setup(repo => repo.Schedule(It.IsAny<Appointment>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeTrue();
        }

        [Test]
        public async Task ScheduleSuccessNotFound()
        {
            // Arrange
            var command = new ScheduleAppointmentCommand
            {
                PatientId = 1,
                DoctorId = 1,
                Date = DateTime.Now.AddDays(1),
                Location = "Sala 101"
            };
            _appointmentRepository
                .Setup(repo => repo.Schedule(It.IsAny<Appointment>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeFalse();
        }

        [Test]
        public async Task ScheduleFailureValidation()
        {
            // Arrange
            var command = new ScheduleAppointmentCommand
            {
                PatientId = 1,
                DoctorId = 1,
                Date = DateTime.Now.AddDays(1),
            };
            _appointmentRepository
                .Setup(repo => repo.Schedule(It.IsAny<Appointment>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNullOrEmpty();

        }
    }
}
