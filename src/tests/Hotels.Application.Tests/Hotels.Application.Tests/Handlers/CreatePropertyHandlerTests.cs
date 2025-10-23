using AutoMapper;
using Hotels.Application.Commands;
using Hotels.Application.DTOs;
using Hotels.Application.Handlers;
using Hotels.Application.Interfaces;
using Hotels.Domain.Entities;
using Hotels.SharedKernel;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Hotels.Application.Tests.Handlers
{
    [TestFixture]
    public class CreatePropertyHandlerTests
    {
        private Mock<IPropertyRepository> _repositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<ILogger<CreatePropertyHandler>> _loggerMock;
        private CreatePropertyHandler _handler;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IPropertyRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<CreatePropertyHandler>>();

            _handler = new CreatePropertyHandler(
                _repositoryMock.Object,
                _mapperMock.Object,
                _loggerMock.Object
            );
        }

        [Test]
        public async Task Handle_ValidRequest_ShouldReturnSuccess()
        {
            // Arrange
            var owner = new Owner { IdOwner = "owner-1", Name = "John Doe" };
            _repositoryMock.Setup(r => r.GetOwnerByIdAsync(It.IsAny<string>()))
                           .ReturnsAsync(owner);
            _repositoryMock.Setup(r => r.AddPropertyAsync(It.IsAny<Property>()))
                           .Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<PropertyDto>(It.IsAny<Property>()))
                       .Returns(new PropertyDto { IdProperty = "prop-1", Name = "Test Property" });

            var command = new CreatePropertyCommand(
                IdProperty: "P03",
                Name: "Casa en el Poblado",
                Street: "Calle 10 #45-23",
                City: "Medellín",
                Country: "Colombia",
                Price: 1250000000m,
                CodeInternal: "CP-2025",
                Year: 2024,
                OwnerId: "O3",
                ImageFile: "https://example.com/casa-poblado.jpg"
            );

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            _repositoryMock.Verify(r => r.AddPropertyAsync(It.IsAny<Property>()), Times.Once);
        }

        [Test]
        public async Task Handle_OwnerNotFound_ShouldReturnFailure()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetOwnerByIdAsync(It.IsAny<string>()))
                       .ReturnsAsync(default(Owner));

            var command = new CreatePropertyCommand(
                IdProperty: "dummy-id",
                Name: "Dummy Property",
                Street: "Dummy Street",
                City: "Dummy City",
                Country: "Dummy Country",
                Price: 0m,
                CodeInternal: "DUMMY",
                Year: 2025,
                OwnerId: "invalid-owner",
                ImageFile: null
            );

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Error, Is.EqualTo("Owner not found"));
        }
    }
}
