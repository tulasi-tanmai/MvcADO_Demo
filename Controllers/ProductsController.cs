using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using MvcADO_Demo.Data;

namespace MvcADO_Demo.Controllers
{

    public class ProductsController : Controller
    {
        private readonly ProductsRepository _productsRepository;
        //above variable holds the refernce to the productsrepository instance
        public ProductsController(ProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
            //above line allows us to use the injected ProductsRepository instance 
            // ie default connection string
        }
        public IActionResult Index()
        {
            var productsdataSet = _productsRepository.GetProducts();
            return View(productsdataSet);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(string name, decimal price)
        {
            if (price >= 0 && ModelState.IsValid)
            {
                var newProductId = _productsRepository.InsertProduct(name, price);

                return RedirectToAction("Index");
            }
            return View();
        }
        

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _productsRepository.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int id, string name, decimal price)
        {
            if (ModelState.IsValid)
            {
                _productsRepository.UpdateProduct(id, name, price);
                return RedirectToAction("Index");
            }
            return View();
        }

    }
    
}