using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyKeeper.Models;
using MoneyKeeper.Dtos;
using MoneyKeeper.Dtos.Category;

namespace MoneyKeeper.Services.Category
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Models.Category>> GetCategories(int walletId);
        public Task<IEnumerable<Models.Category>> GetCategories();
        public Task<Models.Category> CreateCategory(int walletId, CreateCategoryDto categoryDto);
        public Task UpdateCategory(int id, int walletId, UpdateCategoryDto updateCate);
        Task<bool> DeleteCategory(int walletId, int cateId);
        Task<bool> VerifyIsCategoryOfWallet(int categoryId, int walletId);
        //public string SaveIcon(ImageUpload imageUpload);

    }
}
