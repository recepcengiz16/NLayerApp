using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) //herhangi bir filtera takılmayacaksam next deyip yoluma devam ediyorum.
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault(); //idyi yakalamak için gelen istekte. Yani böyle bir id ye sahip karşılık var mı onu kontrol için.
            if (idValue is null)
            {
                await next.Invoke();
                return;
            }

            var id = (int)idValue;
            var anyEntity = await _service.AnyAsync(x => x.Id == id);

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            context.Result =
                new NotFoundObjectResult(
                    CustomResponseDTO<NoContentDTO>.Fail(404, $"{typeof(T).Name}({id}) not found"));
        }
    }
}
