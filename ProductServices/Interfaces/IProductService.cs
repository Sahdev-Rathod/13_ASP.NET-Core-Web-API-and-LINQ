using ProductDAL.Entities;
using ProductViewModel.DTOUiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductServices.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductUi>> GetAllProductsAsync();
        Task<ProductUi> GetProductByIdAsync(int id);
        Task AddProductAsync(ProductUi product);
        Task UpdateProductAsync(int id, ProductUi product);
        Task DeleteProductAsync(int id);
    }
}
