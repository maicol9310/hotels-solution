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
    public class GetPropertiesHandlerTests
    {
        private Mock<IPropertyRepository> _repositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<ILogger<GetPropertiesHandler>> _loggerMock;
        private GetPropertiesHandler _handler;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IPropertyRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<GetPropertiesHandler>>();

            _handler = new GetPropertiesHandler(
                _repositoryMock.Object,
                _mapperMock.Object,
                _loggerMock.Object
            );
        }

        [Test]
        public async Task Handle_ShouldReturnPropertiesList()
        {
            // Arrange
            var properties = new List<Property>
            {
                new Property { IdProperty = "prop-1", Name = "Test Property" },
                new Property { IdProperty = "prop-2", Name = "Another Property" }
            };
            _repositoryMock.Setup(r => r.GetAllPropertiesAsync())
                           .ReturnsAsync(properties);
            _repositoryMock.Setup(r => r.GetPropertyTracesAsync(It.IsAny<string>()))
                           .ReturnsAsync(new List<PropertyTrace>());
            _mapperMock.Setup(m => m.Map<PropertyDto>(It.IsAny<Property>()))
                       .Returns<Property>(p => new PropertyDto { IdProperty = p.IdProperty, Name = p.Name });
            _mapperMock.Setup(m => m.Map<IEnumerable<PropertyTraceDto>>(It.IsAny<IEnumerable<PropertyTrace>>()))
                       .Returns(new List<PropertyTraceDto>());

            var query = new GetPropertiesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.That(result.Value!.Count(), Is.EqualTo(2));
        }
    }
}
