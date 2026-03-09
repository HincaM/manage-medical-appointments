using FluentAssertions;
using Moq;
using NUnit.Framework;
using Persons.Application.Features.Patients.GetAll;
using Persons.Domain.Entities;
using Persons.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Tests.Patients
{
    [TestFixture]
    public class GetAllPatientsQueryHandlerTests
    {
        private GetAllPatientsQueryHandler _handler;
        private Mock<IPatientsRepository> _patientsRepository;

        [SetUp]
        public void Setup()
        {
            _patientsRepository = new Mock<IPatientsRepository>();
            _handler = new GetAllPatientsQueryHandler(_patientsRepository.Object);
        }


        [Test]
        public async Task GetSuccess()
        {
            // Arrange
            var command = new GetAllPatientsQuery();
            var response = new Person
            {
                Id = 1,
                LastName = "Hernandez",
                Identification = "123456789",
                FirstName = "John"
            };

            _patientsRepository
                .Setup(repo => repo.GetAll(It.IsAny<PatientSpecificationBase>(), It.IsAny<CancellationToken>()))
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
            var command = new GetAllPatientsQuery();

            _patientsRepository
                .Setup(repo => repo.GetAll(It.IsAny<PatientSpecificationBase>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((List<Person>)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeNull();
        }        
    }
}
