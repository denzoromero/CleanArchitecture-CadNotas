using ApplicationCore.Common;
using ApplicationCore.UserBS.Queries;
using CleanCadNotas.Configurations;
using CleanCadNotas.Extensions;
using CleanCadNotas.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CleanCadNotas.Controllers
{

    public class HomeController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUser command)
        {
            try
            {
                if (User.Identity?.IsAuthenticated == true)
                {
                    return RedirectToAction(nameof(Index), ControllerExtensions.ControllerName<HomeController>());
                }

                var result = await _mediator.Send(command);
                if (!result.IsSuccess)
                {
                    ViewBag.StatusMessage = new StatusMessageViewModel(false, [result.Error!.Message]);
                    return View();
                }

                return RedirectToAction(nameof(Index), ControllerExtensions.ControllerName<HomeController>());
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

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }



       [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
