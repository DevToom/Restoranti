using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IProductService
    {
        Task<MessageResponse<Product>> Add(Product product);
        Task<MessageResponse<Product>> Update(Product category);
        Task<MessageResponse<Product>> DeleteProduct(int ProductId);
        Task<MessageResponse<List<Product>>> GetList();
        Task<MessageResponse<List<Product>>> GetListByFilters(string Name, int CategoryId, int Status);

    }
}
