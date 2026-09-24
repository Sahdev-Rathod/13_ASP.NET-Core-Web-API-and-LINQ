using ProductDAL.Entities;
using ProductRepositories.Interfaces;
using ProductServices.Interfaces;
using ProductViewModel.DTOUiModel;

namespace ProductServices.Implemaintations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddProductAsync(ProductUi product)
        {
          await _productRepository.AddProductAsync(new Product
           {
               Id = product.Id,
               Name = product.Name,
               Price = product.Price,
               Category = product.Category,
               Quantity = product.Quantity
           });
        }

        public async Task DeleteProductAsync(int id)
        {
           await _productRepository.DeleteProductAsync(id);
        }

        public async Task<IEnumerable<ProductUi>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();

            return products.Select(p => new ProductUi
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Category = p.Category,
                Quantity = p.Quantity
            }).ToList();
        }

        public async Task<ProductUi> GetProductByIdAsync(int id)
        {
           var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return null;
            }
            return new ProductUi
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Category = product.Category,
                Quantity = product.Quantity
            };  
        }

        public async Task UpdateProductAsync(int id, ProductUi product)
        {
            await _productRepository.UpdateProductAsync(id, new Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Category = product.Category,
                Quantity = product.Quantity
            });
        }
    }
}
