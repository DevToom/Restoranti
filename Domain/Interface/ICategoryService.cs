using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface ICategoryService
    {
        Task<MessageResponse<Category>> Add(Category category);
        Task<MessageResponse<Category>> Update(Category category);
        Task<MessageResponse<Category>> DeleteCategory(int CategoryId); 
        Task<MessageResponse<List<Category>>> GetList();
        Task<MessageResponse<List<Category>>> GetListByFilters(string Name, int MenuType, int Status);
        
    }


}
