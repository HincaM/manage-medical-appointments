using FluentAssertions;
using Moq;
using NUnit.Framework;
using Persons.Application.Features.Patients.Delete;
using Persons.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Tests.Patients
{
    [TestFixture]
    public class DeletePatientCommandHandlerTests
    {
        private DeletePatientCommandHandler _handler;
        private Mock<IPatientsRepository> _patientsRepository;

        [SetUp]
        public void Setup()
        {
            _patientsRepository = new Mock<IPatientsRepository>();
            _handler = new DeletePatientCommandHandler(_patientsRepository.Object);
        }


        [Test]
        public async Task DeleteSuccess()
        {
            // Arrange
            var command = new DeletePatientCommand
            {
                Id = 1
            };

            _patientsRepository
                .Setup(repo => repo.Delete(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeTrue();
        }

        [Test]
        public async Task DeleteFailureValidation()
        {
            // Arrange
            var command = new DeletePatientCommand
            {
                Id = 1
            };

            _patientsRepository
                .Setup(repo => repo.Delete(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeFalse();
            result.Error.Should().NotBeNull();
        }

        [Test]
        public async Task DeleteFailure()
        {
            // Arrange
            var command = new DeletePatientCommand
            {
                Id = 1
            };

            _patientsRepository
                .Setup(repo => repo.Delete(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeFalse();
        }
    }
}



