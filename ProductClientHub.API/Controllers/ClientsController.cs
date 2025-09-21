using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionsBase;
using ProductClientHub.API.UseCases.Clients.Register;

namespace ProductClientHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly RegisterClientUseCase _useCase;

        public ClientsController(RegisterClientUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseClientJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RequestClientJson request)
        {
            try
            {
                var response = _useCase.Execute(request);
                return Created(string.Empty, response);
            }
            catch (ProductClientHubException ex)
            {
                return BadRequest(new ResponseErrorMessagesJson(ex.GetErrors()));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseErrorMessagesJson("ERRO DESCONHECIDO"));
            }
        }

        [HttpPut]
        public IActionResult Update() => Ok();

        [HttpGet]
        public IActionResult GetAll() => Ok();

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id) => Ok();

        [HttpDelete]
        public IActionResult Delete() => Ok();
    }
}
