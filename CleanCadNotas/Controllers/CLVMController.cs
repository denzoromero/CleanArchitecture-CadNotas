using ApplicationCore.CadNotasCore.CLVMs.Commands.Create;
using ApplicationCore.CadNotasCore.CLVMs.Commands.Deactivate;
using ApplicationCore.CadNotasCore.CLVMs.Commands.Export;
using ApplicationCore.CadNotasCore.CLVMs.Commands.Update;
using ApplicationCore.CadNotasCore.CLVMs.Queries;
using ApplicationCore.CadNotasCore.NotaFiscaisCore.Queries;
using ApplicationCore.CadNotasCore.Projetos.Queries;
using CleanCadNotas.Configurations;
using CleanCadNotas.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CleanCadNotas.Controllers
{
    public class CLVMController(IMediator mediator) : Controller
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

        public async Task<IActionResult> Search(SearchNotaFiscais query)
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



        public async Task<IActionResult> CLVMPage(GetCLVMPage query) 
        {
            try
            {
                var result = await _mediator.Send(query);
                if (!result.IsSuccess) return BadRequest(new { Type = "Business", Message = result.Error!.Message });

                ViewBag.LVM = result.Value?.Nota;
                ViewBag.Procedimentos = result.Value?.Procedimentos;
                ViewBag.CLVMs = result.Value?.CLVMs;
                ViewBag.IdempotencyKey = Guid.NewGuid().ToString();

                return View();
                //return Ok(result.Value);
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

        public async Task<IActionResult> GetCLVMData(GetCLVMbyId query)
        {
            try
            {
                var result = await _mediator.Send(query);

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
        public async Task<IActionResult> EditProcedimento([FromBody] UpdateLVMProcedimento command)
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
        public async Task<IActionResult> Insert([FromBody] CreateCLVM command)
        {
            try
            {
                var result = await _mediator.Send(command);
                if (!result.IsSuccess) return BadRequest(new { Type = "Business", Message = result.Error!.Message });

                return Ok(new { Message = result.Message, RedirectUrl = Url.Action(nameof(CLVMPage), new { id = command.IdLVM }) });
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
        public async Task<IActionResult> Edit([FromBody] UpdateCLVM command)
        {
            try
            {
                var result = await _mediator.Send(command);
                if (!result.IsSuccess) return BadRequest(new { Type = "Business", Message = result.Error!.Message });

                return Ok(new { Message = result.Message, RedirectUrl = Url.Action(nameof(CLVMPage), new { id = command.IdLVM }) });
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
        public async Task<IActionResult> Deactivate(DeactivateCLVM command)
        {
            try
            {
                var result = await _mediator.Send(command);
                if (!result.IsSuccess) return BadRequest(new { Type = "Business", Message = result.Error!.Message });

                return Ok(new { Message = result.Message, RedirectUrl = Url.Action(nameof(CLVMPage), new { id = command.IdLVM }) });
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

        public async Task<IActionResult> ExportControlTub(ExportControlTubExcel command)
        {
            try
            {
                var excelBytes = await _mediator.Send(command);

                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ControlTub.xlsx");
            }
            catch (ValidationException ex)
            {
                ViewBag.StatusMessage = new StatusMessageViewModel(false, ex.Errors.Select(x => x.ErrorMessage));
                return View(nameof(Index));
            }
            catch (Exception ex) when (TimeoutExceptionHandler.IsSqlTimeout(ex))
            {
                ViewBag.StatusMessage = new StatusMessageViewModel(false, ["Execution Timeout Expired."]);
                return View(nameof(Index));
            }
        }

        public async Task<IActionResult> ExportControlStru(ExportControlStruExcel command)
        {
            try
            {
                var excelBytes = await _mediator.Send(command);

                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ControlStru.xlsx");
            }
            catch (ValidationException ex)
            {
                ViewBag.StatusMessage = new StatusMessageViewModel(false, ex.Errors.Select(x => x.ErrorMessage));
                return View(nameof(Index));
            }
            catch (Exception ex) when (TimeoutExceptionHandler.IsSqlTimeout(ex))
            {
                ViewBag.StatusMessage = new StatusMessageViewModel(false, ["Execution Timeout Expired."]);
                return View(nameof(Index));
            }
        }

        public async Task<IActionResult> PrintCLVMPage(int IdLVM)
        {
            try
            {
                return View();
            }
            catch (ValidationException ex)
            {
                ViewBag.StatusMessage = new StatusMessageViewModel(false, ex.Errors.Select(x => x.ErrorMessage));
                return View(nameof(Index));
            }
            catch (Exception ex) when (TimeoutExceptionHandler.IsSqlTimeout(ex))
            {
                ViewBag.StatusMessage = new StatusMessageViewModel(false, ["Execution Timeout Expired."]);
                return View(nameof(Index));
            }
        }

        public async Task<IActionResult> SearchCodigo(GetCodigos command)
        {
            try
            {
                var result = await _mediator.Send(command);

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

    }
}
