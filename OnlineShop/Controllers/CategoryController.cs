using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers
{
    public class CategoryController : Controller
    {
        public CategoryController(CategoryService service, ProductService productService)
        {
            Service = service;
            ProductService = productService;
        }

        public CategoryService Service { get; }
        public ProductService ProductService { get; }

        public async Task<IActionResult> Index()
        {
            var list = await Service.GetListAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (category.Name.Length < 3)
                ModelState.AddModelError("Name", "عنوان دسته بندی باید بیشتر از 3 حرف باشد");

            if (ModelState.IsValid)
            {
                await Service.Create(category);
                RedirectToAction("Index");
            }
            return View(category);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var category = await Service.Get((int)id);
            if (category == null) return NotFound();
            return View(category);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Category category)
        {

            if (id == null) return NotFound();
            if (ModelState.IsValid)
            {
                var editedCategory = Service.EditCategory(category);
                return View(editedCategory);

            }
            return View(category);
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await Service.Get(id);
            return View(category);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var category = Service.Get((int)id);
            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await Service.Get(id);
            if (category != null) Service.RemoveCategory(category);
            return RedirectToAction(nameof(Index));
        }
    }

}
