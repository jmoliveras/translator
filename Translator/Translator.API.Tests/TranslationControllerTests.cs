using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Translator.Controllers;
using Translator.Application.Queries;
using Translator.Application.Commands;
using Translator.Application.DTO;

namespace Translator.API.Tests
{
    public class TranslationControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly TranslationController _controller;

        public TranslationControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new TranslationController(_mockMediator.Object);
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
        public void Create_ModelStateValid_MediatorSendCalledOnce()
        {
            var id = Guid.NewGuid();

            _mockMediator.Setup(x => x.Send(It.IsAny<CreateTranslationCommand>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(new Unit());

            var result = _controller.CreateTranslation("Test");

            _mockMediator.Verify(x => x.Send(It.IsAny<CreateTranslationCommand>(),
               It.IsAny<CancellationToken>()), Times.Once);

            Assert.IsType<ActionResult<Guid>>(result);
        }

        private TranslationDto GetTestTranslation() => new TranslationDto
        {
            DetectedLanguage = "es",
            OriginalText = "Test",
            Result = "Prueba"
        };
    }
}