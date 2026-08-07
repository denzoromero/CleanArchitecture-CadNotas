using ApplicationCore.CadNotasCore.Procedimentos.Commands.Create;
using ApplicationCore.CadNotasCore.Procedimentos.Commands.Update;
using ApplicationCore.CadNotasCore.Procedimentos.Queries;
using ApplicationCore.CadNotasCore.Projetos.Queries;
using CleanCadNotas.Configurations;
using CleanCadNotas.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanCadNotas.Controllers
{
    public class ProcedimentoController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;
        public async Task<IActionResult> Index(GetObraList query)
        {
            try
            {
                var obras = await _mediator.Send(query);
                ViewBag.ObraList = obras;

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

        public async Task<IActionResult> Search(SearchProcedimentos query)
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
        public async Task<IActionResult> Insert([FromBody] CreateProcedimento command)
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
        public async Task<IActionResult> Edit([FromBody] UpdateProcedimento command)
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
