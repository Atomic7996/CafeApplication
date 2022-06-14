using Microsoft.AspNetCore.Mvc;
using CafeWeb.Data.interfaces;
using ClassLibraryCafe;
using System.Linq;
using CafeWeb.ViewModels;
using CafeWeb.Data;
using System.Collections.Generic;

namespace CafeWeb.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProducts _prods;
        private readonly AppDBContext _appDBContext;

        public ProductsController(IProducts iProducts, AppDBContext context)
        {
            _prods = iProducts;
            _appDBContext = context;
        }

        public ViewResult List()
        {
            ViewBag.Title = "Блюда";

            List<Product> products = new List<Product>();
            try
            {
                products = _appDBContext.Product.ToList();
            }
            catch (System.Exception)
            {

            }

            return View(products);
        }
    }
}
