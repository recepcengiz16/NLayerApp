using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Web.Filter;
using NLayer.Web.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;

        public ProductsController(ProductApiService productApiService, CategoryApiService categoryApiService)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _productApiService.GetProductsWithCategoryAsync());
        }

        public async Task<IActionResult> Save()
        {
            var categoriesDTO = await _categoryApiService.GetAllAsync();

           

            ViewBag.Categories = new SelectList(categoriesDTO, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDTO productDTO)
        {

            if (ModelState.IsValid)
            {
                await _productApiService.SaveAsync(productDTO);
                return RedirectToAction(nameof(Index));
            }

            var categoriesDTO = await _categoryApiService.GetAllAsync();

            ViewBag.Categories = new SelectList(categoriesDTO, "Id", "Name");

            return View();
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productApiService.GetByIdAsync(id);

            var categoriesDTO = await _categoryApiService.GetAllAsync();

            ViewBag.Categories = new SelectList(categoriesDTO, "Id", "Name", product.CategoryId);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.UpdateAsync(product);
                return RedirectToAction(nameof(Index));
            }

            var categoriesDTO = await _categoryApiService.GetAllAsync();

            ViewBag.Categories = new SelectList(categoriesDTO, "Id", "Name", product.CategoryId);

            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {

           await _productApiService.RemoveAsync(id);

            return RedirectToAction(nameof(Index));

        }
    }
}
