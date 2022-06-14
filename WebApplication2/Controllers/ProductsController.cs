using CafeWebApp.Data.Interfaces;
using CafeWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CafeWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProducts _allProducts;

        public ProductsController(IProducts products)
        {
            _allProducts = products;
        }

        public ViewResult ListProducts()
        {
            ViewBag.Title = "Блюда";
            ProductsListViewModel obj = new ProductsListViewModel();
            obj.getAllProducts = _allProducts.AllProducts;

            return View(obj);
        }
    }
}
