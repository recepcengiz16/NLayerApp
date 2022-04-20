using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
   
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();

            var categoriesDTO = _mapper.Map<List<CategoryDTO>>(categories.ToList());

            return CreateActionResult(CustomResponseDTO<List<CategoryDTO>>.Success(200,categoriesDTO));


        }



        [HttpGet("[action]/{categoryID}")]
        public async Task<IActionResult> GetSingleCategoryByIdWithProductsAsync(int categoryID)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryByIdWithProductsAsync(categoryID));
        }
    }
}
