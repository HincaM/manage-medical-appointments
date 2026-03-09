using FluentAssertions;
using Moq;
using NUnit.Framework;
using Persons.Application.Features.Doctors.Delete;
using Persons.Domain.Entities;
using Persons.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Tests.Doctors
{
    [TestFixture]
    public class DeleteDoctorCommandHandlerTests
    {
        private DeleteDoctorCommandHandler _handler;
        private Mock<IDoctorsRepository> _doctorsRepository;

        [SetUp]
        public void Setup()
        {
            _doctorsRepository = new Mock<IDoctorsRepository>();
            _handler = new DeleteDoctorCommandHandler(_doctorsRepository.Object);
        }


        [Test]
        public async Task DeleteSuccess()
        {
            // Arrange
            var command = new DeleteDoctorCommand
            {
                Id = 1
            };

            _doctorsRepository
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
            var command = new DeleteDoctorCommand
            {
                Id = 1
            };

            _doctorsRepository
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
            var command = new DeleteDoctorCommand
            {
                Id = 1
            };

            _doctorsRepository
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



