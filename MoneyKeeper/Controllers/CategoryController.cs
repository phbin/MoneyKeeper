using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Data;
using MoneyKeeper.Dtos.Category;
using MoneyKeeper.Models;
using MoneyKeeper.Services.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyKeeper.Controllers
{
    [ApiController]
    [Route("/api/categories")]
    public class CategoryController : Controller
    {
        public ICategoryService _categoryService { get; set; }
        public IMapper _mapper { get; set; }
        public CategoryController(ICategoryService category, DataContext context, IMapper mapper)
        {
            _categoryService = category;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetStandardCategory()
        {
            var cates = await _categoryService.GetCategories();
            var catesRes = _mapper.Map<IEnumerable<CategoryDto>>(cates);
            return Ok(new ApiResponse<IEnumerable<CategoryDto>>(catesRes, "Get categories successfully!"));
        }


        //[HttpDelete("{id}")]
        //[Produces(typeof(NoContentResult))]
        //public async Task<IActionResult> DeleteCategory(int id)
        //{
        //    var userId = HttpContext.Items["UserId"] as int?;
        //    await _categoryService.DeleteCategory(id, walletId);
        //    return NoContent();
        //}
    }
}
