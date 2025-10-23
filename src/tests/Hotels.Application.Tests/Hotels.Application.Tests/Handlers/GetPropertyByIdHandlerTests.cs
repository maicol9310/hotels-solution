using Moq;
using AutoMapper;
using Hotels.Application.Handlers;
using Hotels.Application.Queries;
using Hotels.Application.DTOs;
using Hotels.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Hotels.Domain.Entities;

namespace Hotels.Application.Tests.Handlers
{
    [TestFixture]
    public class GetPropertyByIdHandlerTests
    {
        private Mock<IPropertyRepository> _repositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<ILogger<GetPropertyByIdHandler>> _loggerMock;
        private GetPropertyByIdHandler _handler;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IPropertyRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<GetPropertyByIdHandler>>();

            _handler = new GetPropertyByIdHandler(
                _repositoryMock.Object,
                _mapperMock.Object,
                _loggerMock.Object
            );
        }

        [Test]
        public async Task Handle_PropertyExists_ShouldReturnSuccess()
        {
            // Arrange
            var property = new Property { IdProperty = "prop-1", Name = "Test Property" };
            _repositoryMock.Setup(r => r.GetPropertyByIdAsync("prop-1"))
                           .ReturnsAsync(property);
            _repositoryMock.Setup(r => r.GetPropertyTracesAsync("prop-1"))
                           .ReturnsAsync(new List<PropertyTrace>());
            _mapperMock.Setup(m => m.Map<PropertyDto>(property))
                       .Returns(new PropertyDto { IdProperty = "prop-1", Name = "Test Property" });
            _mapperMock.Setup(m => m.Map<List<PropertyTraceDto>>(It.IsAny<IEnumerable<PropertyTrace>>()))
                       .Returns(new List<PropertyTraceDto>());

            var query = new GetPropertyByIdQuery("prop-1"); // ✔ constructor posicional

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value!.IdProperty, Is.EqualTo("prop-1"));
        }

        [Test]
        public async Task Handle_PropertyNotFound_ShouldReturnFailure()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetPropertyByIdAsync("invalid-id"))
                           .ReturnsAsync((Property?)null);

            var query = new GetPropertyByIdQuery("invalid-id"); // ✔ constructor posicional

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Error, Does.Contain("not found"));
        }

    }
}
