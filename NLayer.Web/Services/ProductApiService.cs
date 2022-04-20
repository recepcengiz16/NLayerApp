using NLayer.Core.DTOs;

namespace NLayer.Web.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _client;

        public ProductApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<ProductWithCategoryDTO>> GetProductsWithCategoryAsync()
        {
            var response = await _client.GetFromJsonAsync<CustomResponseDTO<List<ProductWithCategoryDTO>>>("products/GetProductWithCategory");
            return response.Data;
        }

        public async Task<ProductDTO> SaveAsync(ProductDTO product)
        {
            var response = await _client.PostAsJsonAsync("products", product);

            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDTO<ProductDTO>>();

            return responseBody.Data; 
            
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            var response = await _client.GetFromJsonAsync<CustomResponseDTO<ProductDTO>>($"products/{id}");
            return response.Data;
        }

        public async Task<bool> UpdateAsync(ProductDTO product)
        {
            var response = await _client.PutAsJsonAsync("products", product);

            return response.IsSuccessStatusCode;

        }

        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _client.DeleteAsync($"products/{id}");

            return response.IsSuccessStatusCode;

        }
    }
}
