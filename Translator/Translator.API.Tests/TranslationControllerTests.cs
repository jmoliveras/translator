using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Translator.Controllers;
using Translator.Application.Queries;
using Translator.Application.Commands;
using Translator.Application.DTO;
using Microsoft.Extensions.Configuration;

namespace Translator.API.Tests
{
    public class TranslationControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly TranslationController _controller;

        public TranslationControllerTests()
        {
            _mockMediator = new Mock<IMediator>();

            Dictionary<string, string?> inMemorySettings = new()
            {
                {"TranslationMaxLength", "5000"},
            };

            Assert.NotNull(inMemorySettings);

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _controller = new TranslationController(_mockMediator.Object, configuration);
        }

        [Fact]
        public async void GetTranslationTest()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<GetTranslationByIdQuery>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(GetTestTranslation());

            var id = Guid.NewGuid();
            var result = await _controller.Get(id);

            _mockMediator.Verify(x => x.Send(It.IsAny<GetTranslationByIdQuery>(),
               It.IsAny<CancellationToken>()), Times.Once);

            Assert.Equal(GetTestTranslation(), result);
        }      

        [Fact]
        public async void Create_ModelStateValid_MediatorSendCalledOnce()
        {
            var result = await _controller.CreateTranslation("Test");

            _mockMediator.Verify(x => x.Send(It.IsAny<CreateTranslationCommand>(),
               It.IsAny<CancellationToken>()), Times.Once);

            Assert.IsType<ActionResult<Guid>>(result);
            Assert.Equal(200, (result.Result as ObjectResult)?.StatusCode);
        }

        [Fact]
        public async void Create_TextExceedsLength_ReturnBadRequest()
        {                   
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    {"TranslationMaxLength", "5"},
                }).Build();

            var controller = new TranslationController(_mockMediator.Object, configuration);
            var result = await controller.CreateTranslation("Too long text");

            Assert.Equal(400, (result.Result as ObjectResult)?.StatusCode);
        }

        private TranslationDto GetTestTranslation() => new TranslationDto
        {
            DetectedLanguage = "es",
            OriginalText = "Test",
            Result = "Prueba"
        };
    }
}