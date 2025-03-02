using Microsoft.AspNetCore.Mvc;
using Webbanson.Data;

namespace Webbanson.ViewCompoments
{
    public class ListProducts : ViewComponent
    {
        private readonly ComesticsContext db;

        public ListProducts(ComesticsContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Categories.Select(lo => new
            {
                lo.CategoryId,
                lo.CategoryName,
            }).ToList();

            return View(data);
        }
    }
}