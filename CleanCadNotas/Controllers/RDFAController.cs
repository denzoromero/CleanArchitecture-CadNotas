using ApplicationCore.CadNotasCore.CLVMs.VMs;
using ApplicationCore.CadNotasCore.Projetos.Queries;
using ApplicationCore.CadNotasCore.RDFAs.Queries.GetLVMRDFA;
using ApplicationCore.CadNotasCore.RDFAs.Queries.GetRDFAPage;
using ApplicationCore.Common;
using CleanCadNotas.Configurations;
using CleanCadNotas.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanCadNotas.Controllers
{
    public class RDFAController(IMediator mediator) : Controller
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

        public async Task<IActionResult> Search(GetLVMRDFAQuery query)
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

        public async Task<IActionResult> RDFAPage(GetRDFAPageQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);
                if (!result.IsSuccess)
                {
                    ViewBag.StatusMessage = new StatusMessageViewModel(false, [result.Error!.Message]);
                    return View();
                }
                ViewBag.LVM = result.Value?.Nota;
                ViewBag.CLVMs = result.Value?.CLVMs;

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


    }
}
