using System.Collections.Generic;
using System.Web.Mvc;
using ListModelBinding.Models;

namespace ListModelBinding.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            var viewModel = new ViewModel
            {
                SelectListItems = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Wert1", Value = "1"},
                    new SelectListItem {Text = "Wert2", Value = "2"},
                    new SelectListItem {Text = "Wert3", Value = "3"}
                },
                Items = new List<ItemModel>
                {
                    new ItemModel {Id = 1, Description = "bla1", Name = "Name1"},
                    new ItemModel {Id = 2, Description = "bla2", Name = "Name2"},
                    new ItemModel {Id = 3, Description = "bla3", Name = "Name3"}
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult PostData(List<ItemModel> list)
        {
            bool isValid = ModelState.IsValid;
            return new EmptyResult();
        }

        public ActionResult PostSimple(Single single)
        {
            bool isValid = ModelState.IsValid;
            bool isAjaxRequest = Request.IsAjaxRequest();
            return new EmptyResult();
        }
    }
}
