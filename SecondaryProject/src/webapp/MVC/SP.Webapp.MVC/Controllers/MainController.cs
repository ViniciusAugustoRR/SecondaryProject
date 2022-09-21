using Microsoft.AspNetCore.Mvc;
using SP.Webapp.MVC.Models;

namespace SP.Webapp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponseHasErrors(ResponseResult response)
        {
            if(response != null && response.Errors.Mensagens.Any())
            {
                foreach(var mensagem in response.Errors.Mensagens)
                {
                    ModelState.AddModelError(string.Empty, mensagem);
                }

                return true;
            }

            return false;
        }

    }
}
