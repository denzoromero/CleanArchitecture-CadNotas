using ApplicationCore.CadNotasCore.Obras.Commands.Create;
using ApplicationCore.CadNotasCore.Obras.Commands.Update;
using ApplicationCore.CadNotasCore.Obras.Queries;
using CleanCadNotas.Configurations;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanCadNotas.Controllers
{
    [Authorize]
    public class ObraController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Search(SearchObra query)
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
        public async Task<IActionResult> Insert([FromBody] CreateObra command)
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
        public async Task<IActionResult> Edit([FromBody] UpdateObra command)
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
