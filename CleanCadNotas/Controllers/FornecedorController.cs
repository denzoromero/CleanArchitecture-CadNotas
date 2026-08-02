using ApplicationCore.BSCore.StateCity;
using ApplicationCore.CadNotasCore.Fornecedors.Commands.Create;
using ApplicationCore.CadNotasCore.Fornecedors.Commands.Update;
using ApplicationCore.CadNotasCore.Fornecedors.Queries;
using CleanCadNotas.Configurations;
using CleanCadNotas.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CleanCadNotas.Controllers
{
    [Authorize]
    public class FornecedorController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [TempData]
        public string? StatusMessage { get; set; }

        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new StateQuery());
            ViewBag.StateList = result;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Search(SearchFornecedorQuery query)
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
        public async Task<IActionResult> Insert([FromBody] CreateFornecedor command)
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
        public async Task<IActionResult> Edit([FromBody] UpdateFornecedor command)
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
