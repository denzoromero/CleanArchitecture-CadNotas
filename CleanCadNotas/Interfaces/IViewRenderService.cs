using Microsoft.AspNetCore.Mvc;

namespace CleanCadNotas.Interfaces
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(ActionContext actionContext,string viewName,object? model = null);
    }
}
