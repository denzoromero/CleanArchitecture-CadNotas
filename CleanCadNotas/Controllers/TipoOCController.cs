using ApplicationCore.CadNotasCore.TipoOCs.Commands.Create;
using ApplicationCore.CadNotasCore.TipoOCs.Commands.Update;
using ApplicationCore.CadNotasCore.TipoOCs.Queries;
using CleanCadNotas.Configurations;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanCadNotas.Controllers
{
    [Authorize]
    public class TipoOCController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Search(SearchTipoOC query)
        {
            try
            {
                var result = await _mediator.Send(query);
                if (!result.IsSuccess) return BadRequest(new { Type = "Business", Message = result.Error!.Message });

                return Ok(result.Value);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Type = "Validation", Errors = ex.Errors });
            }
            catch (Exception ex) when (TimeoutExceptionHandler.IsSqlTimeout(ex))
            {
                return StatusCode(503, new { Type = "Infrastructure", Message = "Execution Timeout Expired." });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CreateTipoOC command)
        {
            try
            {
                var result = await _mediator.Send(command);
                if (!result.IsSuccess) return BadRequest(new { Type = "Business", Message = result.Error!.Message });

                return Ok(new { Message = result.Message, RedirectUrl = Url.Action(nameof(Index)) });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Type = "Validation", Errors = ex.Errors });
            }
            catch (Exception ex) when (TimeoutExceptionHandler.IsSqlTimeout(ex))
            {
                return StatusCode(503, new { Type = "Infrastructure", Message = "Execution Timeout Expired." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] UpdateTipoOC command)
        {
            try
            {
                var result = await _mediator.Send(command);
                if (!result.IsSuccess) return BadRequest(new { Type = "Business", Message = result.Error!.Message });

                return Ok(new { Message = result.Message, RedirectUrl = Url.Action(nameof(Index)) });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Type = "Validation", Errors = ex.Errors });
            }
            catch (Exception ex) when (TimeoutExceptionHandler.IsSqlTimeout(ex))
            {
                return StatusCode(503, new { Type = "Infrastructure", Message = "Execution Timeout Expired." });
            }
        }


    }
}
