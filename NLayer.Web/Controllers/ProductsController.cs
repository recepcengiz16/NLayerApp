using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Web.Filter;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _service;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService service, ICategoryService categoryService, IMapper mapper)
        {
            _service = service;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _service.GetProductsWithCategory());
        }

        public async Task<IActionResult> Save()
        {
            var categoires = await _categoryService.GetAllAsync();

            var categoriesDTO = _mapper.Map<List<CategoryDTO>>(categoires.ToList());

            ViewBag.Categories = new SelectList(categoriesDTO, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDTO productDTO)
        {

            if (ModelState.IsValid)
            {
                await _service.AddAsync(_mapper.Map<Product>(productDTO));
                return RedirectToAction(nameof(Index));
            }

            var categoires = await _categoryService.GetAllAsync();

            var categoriesDTO = _mapper.Map<List<CategoryDTO>>(categoires.ToList());

            ViewBag.Categories = new SelectList(categoriesDTO, "Id", "Name");

            return View();
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _service.GetByIdAsync(id);

            var categoires = await _categoryService.GetAllAsync();

            var categoriesDTO = _mapper.Map<List<CategoryDTO>>(categoires.ToList());

            ViewBag.Categories = new SelectList(categoriesDTO, "Id", "Name", product.CategoryId);

            return View(_mapper.Map<ProductDTO>(product));
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(_mapper.Map<Product>(product));
                return RedirectToAction(nameof(Index));
            }

            var categoires = await _categoryService.GetAllAsync();

            var categoriesDTO = _mapper.Map<List<CategoryDTO>>(categoires.ToList());

            ViewBag.Categories = new SelectList(categoriesDTO, "Id", "Name", product.CategoryId);

            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.GetByIdAsync(id);

           await _service.RemoveAsync(product);

            return RedirectToAction(nameof(Index));

        }
    }
}
