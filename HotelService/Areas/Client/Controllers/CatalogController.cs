using HotelService.Models.ViewModels.Client;
using HotelService.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Roles = "Developer, Client")]
    public class CatalogController : Controller
    {
        private ICatalogManager m_Catalog;
        public CatalogController(ICatalogManager catalog)
        {
            m_Catalog = catalog;
        }

        public ViewResult Index()
        {
            var Catalog = new CatalogModel
            {
                Services = m_Catalog.Services,
                Categories = m_Catalog.Categories
            };
            return View(Catalog);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var Error in result.Errors)
            {
                ModelState.AddModelError("", Error.Description);
            }
        }
    }
}
