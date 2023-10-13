using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly CategoryService categoryService;

        public ProductController(ProductService service, CategoryService categoryService)
        {
            Service = service;
            this.categoryService = categoryService;
        }

        public ProductService Service { get; }

        public async Task<IActionResult> Index()
        {
            var list = await Service.GetListAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var allcategories = await categoryService.GetListAsync();
            ViewData["CategoryId"] = new SelectList(allcategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product Product)
        {
            if (Product.Name.Length < 3)
                ModelState.AddModelError("Name", "عنوان محصول باید بیشتر از 3 حرف باشد");
            if (Product.CategoryId <= 0)
                ModelState.AddModelError("CategoryId", "ثبت دسته بندی درمحصول اجباری است");

            if (ModelState.IsValid)
            {
                await Service.Create(Product);
                RedirectToAction("Index");
            }
            return View(Product);
        }
        public async Task<IActionResult> Details(int id)
        {
            var product = await Service.GetAsync(id);
            //var productList = await ProductService.GetListAsync();
            //var targetproducts = productList.Where(e => e.CategoryId == id).ToList();
            //category.ProductList = targetproducts;

            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var product = await Service.Get((int)id);
            if (product == null) return NotFound();
            return View(product);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Product product)
        {

            if (id == null) return NotFound();
            if (ModelState.IsValid)
            {
                var editedCategory = Service.EditProduct(product);
                return View(editedCategory);

            }
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var product = await Service.Get((int)id);
            if (product == null) return NotFound();
            return View(product);

        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            var product = await Service.Get(id);
            if (product != null) Service.RemoveProduct(product);

            return RedirectToAction(nameof(Index));
        }

    }
}
