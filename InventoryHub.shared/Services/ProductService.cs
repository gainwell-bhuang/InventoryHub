using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryHub.shared.Models;

namespace InventoryHub.shared.Services
{
    public class ProductService
    {
        private Product[]? _cachedProducts;

        public async Task<Product[]> GetProductsAsync(HttpClient httpClient)
        {
            if (_cachedProducts != null)
            {
                return _cachedProducts;
            }

            var response = await httpClient.GetAsync("/api/productlist");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            _cachedProducts = System.Text.Json.JsonSerializer.Deserialize<Product[]>(json, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return _cachedProducts!;
        }
    }
}