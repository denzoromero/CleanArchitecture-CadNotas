using ApplicationCore.BSCore.StateCity;
using ApplicationCore.CadNotasCore.Transportadoras.Commands.Create;
using ApplicationCore.CadNotasCore.Transportadoras.Commands.Update;
using ApplicationCore.CadNotasCore.Transportadoras.Queries;
using CleanCadNotas.Configurations;
using CleanCadNotas.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanCadNotas.Controllers
{
    [Authorize]
    public class TransportadoraController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _mediator.Send(new StateQuery());
                ViewBag.StateList = result;

                return View();
            }
            catch (ValidationException ex)
            {
                ViewBag.StatusMessage = new StatusMessageViewModel(false, ex.Errors.Select(x => x.ErrorMessage));
                return View();
            }
            catch (Exception ex) when (TimeoutExceptionHandler.IsSqlTimeout(ex))
            {
                ViewBag.StatusMessage = new StatusMessageViewModel(false, ["Execution Timeout Expired."]);
                return View();
            }        
        }

        [HttpGet]
        public async Task<IActionResult> Search(SearchTransportadora query)
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
        public async Task<IActionResult> Insert([FromBody] CreateTransportadora command)
        {
            try
            {
                var result = await _mediator.Send(command);
                if (!result.IsSuccess) return BadRequest(new { Type = "Business", Message = result.Error!.Message });

                return Ok(new { Message = result.Message, RedirectUrl = Url.Action(nameof(Index)) });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Type = "Validation", Errors = ex.Errors.Select(x => x.ErrorMessage) });
            }
            catch (Exception ex) when (TimeoutExceptionHandler.IsSqlTimeout(ex))
            {
                return StatusCode(503, new { Type = "Infrastructure", Message = "Execution Timeout Expired." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] UpdateTransportadora command)
        {
            try
            {
                var result = await _mediator.Send(command);
                if (!result.IsSuccess) return BadRequest(new { Type = "Business", Message = result.Error!.Message });

                return Ok(new { Message = result.Message, RedirectUrl = Url.Action(nameof(Index)) });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Type = "Validation", Errors = ex.Errors.Select(x => x.ErrorMessage) });
            }
            catch (Exception ex) when (TimeoutExceptionHandler.IsSqlTimeout(ex))
            {
                return StatusCode(503, new { Type = "Infrastructure", Message = "Execution Timeout Expired." });
            }
        }


    }
}
