using FluentAssertions;
using Moq;
using NUnit.Framework;
using Persons.Application.Features.Patients.Update;
using Persons.Domain.Entities;
using Persons.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Tests.Patients
{
    [TestFixture]
    public class UpdatePatientCommandHandlerTests
    {
        private UpdatePatientCommandHandler _handler;
        private Mock<IPatientsRepository> _patientsRepository;

        [SetUp]
        public void Setup()
        {
            _patientsRepository = new Mock<IPatientsRepository>();
            _handler = new UpdatePatientCommandHandler(_patientsRepository.Object);
        }


        [Test]
        public async Task UpdateSuccess()
        {
            // Arrange
            var command = new UpdatePatientCommand
            {
                Id = 1,
                FirstName = "John",
                Identification = "123456789",
                LastName = "Hernandez"
            };

            _patientsRepository
                .Setup(repo => repo.Update(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeTrue();
        }

        [Test]
        public async Task UpdateFailureValidation()
        {
            // Arrange
            var command = new UpdatePatientCommand
            {
                Id = 1
            };

            _patientsRepository
                .Setup(repo => repo.Update(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeFalse();
            result.Error.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateFailure()
        {
            // Arrange
            var command = new UpdatePatientCommand
            {
                Id = 1
            };

            _patientsRepository
                .Setup(repo => repo.Update(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeFalse();
        }
    }
}



