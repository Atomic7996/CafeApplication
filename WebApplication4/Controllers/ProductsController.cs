using Microsoft.AspNetCore.Mvc;
using CafeWeb.Data.interfaces;
using System.Linq;
using CafeWeb.ViewModels;
using CafeWeb.Models;
using CafeWeb.Data;
using System.Collections.Generic;

namespace CafeWeb.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProducts _prods;
        private readonly IProductType _types;
        //private readonly AppDBContext _appDBContext;

        public ProductsController(IProducts iProducts, IProductType itypes, AppDBContext context)
        {
            _prods = iProducts;
            _types = itypes;
            //_appDBContext = context;
        }

        public ViewResult List()
        {
            ViewBag.Title = "Блюда";

            ProductsListViewModel obj = new ProductsListViewModel();
            obj.getAllProducts = _prods.AllProducts;
            obj.currentType = _types.allTypes.FirstOrDefault().ToString();

            return View(obj);
        }
    }
}
