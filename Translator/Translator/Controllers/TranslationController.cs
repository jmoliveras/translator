using MediatR;
using Microsoft.AspNetCore.Mvc;
using Translator.Application.Commands;
using Translator.Application.Queries;
using Translator.Application.Constants;
using Translator.Application.DTO;
using Translator.Domain.Extensions;

namespace Translator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TranslationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public TranslationController(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _configuration = config;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<BaseDto> Get(Guid id)
        {
            return await _mediator.Send(new GetTranslationByIdQuery(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Guid> CreateTranslation([FromBody] string text)
        {
            // Prevent from consuming free tier 2M characters limit in Azure AI Translator
            // by not allowing large texts.
            var maxLength = _configuration.GetValue<int>("TranslationMaxLength");

            if (text.ExceedsLength(maxLength))
            {
                return BadRequest(string.Format(ErrorMessages.TextTooLong, maxLength));
            }

            var id = Guid.NewGuid();
            _ = _mediator.Send(new CreateTranslationCommand { Id = id, OriginalText = text });
            return Ok(id);
        }
    }
}