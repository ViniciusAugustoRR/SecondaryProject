using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SP.Identity.API.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {

        protected ICollection<string> Errors = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (IsOperationValid())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                {"Mensagens", Errors.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach(var error in errors)
            {
                AddToErrorList(error.ErrorMessage);
            }

            return CustomResponse();
        }


        protected bool IsOperationValid()
        {
            return !Errors.Any();
        }

        protected void AddToErrorList(string error)
        {
            Errors.Add(error);
        }

        protected void CleanErrorList()
        {
            Errors.Clear();
        }


    }
}
