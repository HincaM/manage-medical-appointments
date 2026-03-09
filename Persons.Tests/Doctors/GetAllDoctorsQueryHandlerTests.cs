using FluentAssertions;
using Moq;
using NUnit.Framework;
using Persons.Application.Features.Doctors.GetAll;
using Persons.Domain.Entities;
using Persons.Domain.Interfaces;
using Persons.Domain.Specifications;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Tests.Doctors
{
    [TestFixture]
    public class GetAllDoctorsQueryHandlerTests
    {
        private GetAllDoctorsQueryHandler _handler;
        private Mock<IDoctorsRepository> _doctorsRepository;

        [SetUp]
        public void Setup()
        {
            _doctorsRepository = new Mock<IDoctorsRepository>();
            _handler = new GetAllDoctorsQueryHandler(_doctorsRepository.Object);
        }


        [Test]
        public async Task GetSuccess()
        {
            // Arrange
            var command = new GetAllDoctorsQuery();
            var response = new Person
            {
                Id = 1,
                LastName = "Hernandez",
                Identification = "123456789",
                FirstName = "John"
            };

            _doctorsRepository
                .Setup(repo => repo.GetAll(It.IsAny<GetDoctorsSpecification>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Person>() { response });

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
            var command = new GetAllDoctorsQuery();

            _doctorsRepository
                .Setup(repo => repo.GetAll(It.IsAny<GetDoctorsSpecification>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((List<Person>)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeNull();
        }        
    }
}
