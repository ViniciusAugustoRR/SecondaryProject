using Microsoft.AspNetCore.Mvc;

namespace SP.Webapp.MVC.Extension
{
    public class SummaryViewComponent : ViewComponent
    {
            
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }

    }

}
