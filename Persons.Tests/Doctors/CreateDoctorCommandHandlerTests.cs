using FluentAssertions;
using Moq;
using NUnit.Framework;
using Persons.Application.Features.Doctors.Create;
using Persons.Domain.Entities;
using Persons.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Tests.Doctors
{
    [TestFixture]
    public class CreateDoctorCommandHandlerTests
    {
        private CreateDoctorCommandHandler _handler;
        private Mock<IDoctorsRepository> _doctorsRepository;

        [SetUp]
        public void Setup()
        {
            _doctorsRepository = new Mock<IDoctorsRepository>();
            _handler = new CreateDoctorCommandHandler(_doctorsRepository.Object);
        }


        [Test]
        public async Task CreateSuccess()
        {
            // Arrange
            var command = new CreateDoctorCommand
            {
                FirstName = "John",
                Identification = "123456789",
                LastName = "Hernandez"
            };

            _doctorsRepository
                .Setup(repo => repo.Add(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeTrue();
        }

        [Test]
        public async Task CreateFailureValidation()
        {
            // Arrange
            var command = new CreateDoctorCommand
            {
                FirstName = "John",
                LastName = "Hernandez"
            };

            _doctorsRepository
                .Setup(repo => repo.Add(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeFalse();
            result.Error.Should().NotBeNull();
        }

        [Test]
        public async Task CreateFailure()
        {
            // Arrange
            var command = new CreateDoctorCommand
            {
                FirstName = "John",
                Identification = "123456789",
                LastName = "Hernandez"
            };

            _doctorsRepository
                .Setup(repo => repo.Add(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeFalse();
        }
    }
}



