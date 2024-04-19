using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Project.Core;
using System.Linq;

namespace Project.Extensions
{
    public static class ModelStateExtension
    {
        public static ObjectResult InvalidModelStateResponse(ModelStateDictionary modelState)
            => new BadRequestObjectResult(Result<string>.Failure(-400,
                modelState?.Values.First()?.Errors?.First()?.ErrorMessage));
    }
}
