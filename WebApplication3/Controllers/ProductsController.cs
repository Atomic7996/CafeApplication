using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data.Interfaces;
using WebApplication3.Data.ViewModels;

namespace WebApplication3.Controllers
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
            IndexViewModel obj = new IndexViewModel();
            obj.getAllProducts = _allProducts.AllProducts;

            return View(obj);
        }
    }
}
