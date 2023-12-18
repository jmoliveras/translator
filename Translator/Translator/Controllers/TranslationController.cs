using MediatR;
using Microsoft.AspNetCore.Mvc;
using Translator.Application.Commands;
using Translator.Application.Queries;

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
            var id = Guid.NewGuid();
            _ = _mediator.Send(new CreateTranslationCommand { Id = id, Text = text });
            return Ok(id);
        }
    }
}