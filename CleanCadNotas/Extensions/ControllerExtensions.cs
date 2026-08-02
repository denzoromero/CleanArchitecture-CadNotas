using Microsoft.AspNetCore.Mvc;

namespace CleanCadNotas.Extensions
{
    public static class ControllerExtensions
    {
        public static string ControllerName<TController>() where TController : Controller
        {
            return typeof(TController).Name.Replace("Controller", "");
        }
    }
}
