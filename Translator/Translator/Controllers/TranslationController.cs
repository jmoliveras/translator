using MediatR;
using Microsoft.AspNetCore.Mvc;
using Translator.Application.Commands;
using Translator.Application.Queries;
using Translator.Application.Constants;

namespace Translator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TranslationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TranslationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<string> Get(Guid id)
        {
            try
            {
                return await _mediator.Send(new GetTranslationByIdQuery(id));
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Guid> CreateTranslation([FromBody] string text)
        {
            // Prevent from consuming free tier 2M characters limit in Azure AI Translator
            // by not allowing large texts.
            if (text.Length >= 5000)
            {
                return BadRequest(ErrorMessages.TextTooLong);
            }

            var id = Guid.NewGuid();
            _ = _mediator.Send(new CreateTranslationCommand { Id = id, Text = text });
            return Ok(id);
        }
    }
}