using ApplicationCore.BSCore.StateCity;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace CleanCadNotas.Controllers
{
    public class CommonController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetCities(CitiesQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }


        [HttpGet]
        public IActionResult GenerateIdempotencyKey()
        {
            return Ok(new { IdempotencyKey = Guid.NewGuid() });
        }


    }
}
