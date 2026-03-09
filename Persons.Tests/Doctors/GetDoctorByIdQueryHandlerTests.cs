using FluentAssertions;
using Moq;
using NUnit.Framework;
using Persons.Application.Features.Doctors.GetById;
using Persons.Domain.Entities;
using Persons.Domain.Interfaces;
using Persons.Domain.Specifications;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Tests.Doctors
{
    [TestFixture]
    public class GetDoctorByIdQueryHandlerTests
    {
        private GetDoctorByIdQueryHandler _handler;
        private Mock<IDoctorsRepository> _doctorsRepository;

        [SetUp]
        public void Setup()
        {
            _doctorsRepository = new Mock<IDoctorsRepository>();
            _handler = new GetDoctorByIdQueryHandler(_doctorsRepository.Object);
        }


        [Test]
        public async Task GetSuccess()
        {
            // Arrange
            var command = new GetDoctorByIdQuery
            {
                Id = 1,
            };
            var response = new Person 
            { 
                Id = 1, 
                LastName = "Hernandez", 
                Identification = "123456789", 
                FirstName = "John" 
            };

            _doctorsRepository
                .Setup(repo => repo.Get(It.IsAny<GetDoctorSpecification>(), It.IsAny<CancellationToken>()))
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
            var command = new GetDoctorByIdQuery
            {
                Id = 1,
            };

            _doctorsRepository
                .Setup(repo => repo.Get(It.IsAny<GetDoctorSpecification>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Person)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeNull();
        }

        [Test]
        public async Task GetFailureValidation()
        {
            // Arrange
            var command = new GetDoctorByIdQuery
            {
            };

            _doctorsRepository
                .Setup(repo => repo.Get(It.IsAny<GetDoctorSpecification>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Person)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Error.Should().NotBeNull();
        }
    }
}
