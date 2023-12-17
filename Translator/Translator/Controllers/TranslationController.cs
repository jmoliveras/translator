using MediatR;
using Microsoft.AspNetCore.Mvc;
using Translator.Application.Commands;
using Translator.Application.Queries;
using Translator.Application.Responses;

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
        public async Task<ActionResult<TranslationResponse>> CreateTranslation([FromBody] string text)
        {
            var result = await _mediator.Send(new CreateTranslationCommand { Text = text });
            return Ok(result);
        }
    }
}