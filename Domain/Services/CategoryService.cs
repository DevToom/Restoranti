using Domain.Interface;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRCategory _rCategory;
        private readonly IRProduct _rProduct;

        public CategoryService(IRCategory rCategory, IRProduct rProduct)
        {
            this._rCategory = rCategory;
            this._rProduct = rProduct;
        }

        public async Task<MessageResponse<Category>> Add(Category category)
        {
            try
            {
                if (category != null)
                {
                    await _rCategory.AddAsync(category);
                    return new MessageResponse<Category> { Entity = category, Message = "Categoria adicionada com sucesso!" };
                }
                else
                {
                    return new MessageResponse<Category> { HasError = true, Message = "Não há nenhuma informação sobre a nova categoria. (null)" };
                }
            }
            catch (Exception ex)
            {
                return new MessageResponse<Category> { HasError = true, Message = $"Não foi possível adicionar a nova categoria - {ex.Message}" };
            }

        }
        public async Task<MessageResponse<Category>> Update(Category category)
        {
            try
            {
                if (category != null)
                {
                    var existentCategory = await _rCategory.GetById(category.Id);
                    if (existentCategory != null)
                    {
                        existentCategory.Name = category.Name;
                        existentCategory.MenuType = category.MenuType;
                        existentCategory.Status = category.Status;
                        existentCategory.ImageContent = category.ImageContent;
                        existentCategory.ModifiedDate = category.ModifiedDate;
                        existentCategory.ModifiedUserId = category.ModifiedUserId;

                        await _rCategory.Update(existentCategory);

                        return new MessageResponse<Category> { Entity = category, Message = "Categoria atualizada com sucesso!" };
                    }
                    else
                        return new MessageResponse<Category> { Entity = category, Message = "Categoria não encontrada para ser atualizada!" };
                }
                else
                {
                    return new MessageResponse<Category> { HasError = true, Message = "Não há nenhuma informação sobre a categoria para ser atualizada. (null)" };
                }
            }
            catch (Exception ex)
            {
                return new MessageResponse<Category> { HasError = true, Message = $"Não foi possível editar a categoria - {ex.Message}" };
            }

        }
        public async Task<MessageResponse<Category>> DeleteCategory(int CategoryId)
        {
            try
            {
                if (CategoryId != 0)
                {
                    var category = await _rCategory.GetById(CategoryId);
                    if (category != null)
                    {
                        List<Product> existentProducts = _rProduct.GetList().Result;
                        existentProducts = existentProducts.Where(x => x.CategoryId == CategoryId).ToList();

                        if (existentProducts?.Count > 0)
                        {
                            string products = String.Empty;
                            existentProducts.ForEach(x => products += x.Name + ", ");

                            return new MessageResponse<Category> { HasError = true, Message = $@"Não foi possível remover, porque o{(existentProducts.Count > 1 ? "s" : "")} produto{(existentProducts.Count > 1 ? "s" : "")} {products.Substring(0, products.Length - 2)} {(existentProducts.Count > 1 ? "são" : "é")} dessa categoria. Remova-o{(existentProducts.Count > 1 ? "s" : "")} antes!" };
                        }
                        else
                            await _rCategory.Delete(category);

                        return new MessageResponse<Category> { Entity = category, Message = "Categoria removida com sucesso!" };
                    }
                    else
                        return new MessageResponse<Category> { Entity = category, Message = "Categoria não encontrada para ser removida!" };
                }
                else
                {
                    return new MessageResponse<Category> { HasError = true, Message = $"Não há nenhuma categoria com o Id {CategoryId}!" };
                }
            }
            catch (Exception ex)
            {
                return new MessageResponse<Category> { HasError = true, Message = $"Não foi possível excluir a categoria - {ex.Message}" };
            }

        }
        public async Task<MessageResponse<List<Category>>> GetList()
        {
            try
            {
                var entityList = await _rCategory.GetList();
                return new MessageResponse<List<Category>> { Entity = entityList };
            }
            catch (Exception ex)
            {
                return new MessageResponse<List<Category>> { HasError = true, Message = $"Ocorreu um problema ao tentar buscar as informações de categorias. Ex: {ex.Message}" };
            }
        }
        public async Task<MessageResponse<List<Category>>> GetListByFilters(string Name, int MenuType, int Status)
        {
            try
            {
                var entityList = await _rCategory.GetList();
                entityList = entityList.Where(x => x.Name.ToUpper().Contains(Name.ToUpper()) && x.MenuType == (EMenuType)MenuType && x.Status == (ECategoryStatus)Status).ToList();
                entityList = entityList.OrderBy(x => x.Id).ToList();

                return new MessageResponse<List<Category>> { Entity = entityList };
            }
            catch (Exception ex)
            {
                return new MessageResponse<List<Category>> { HasError = true, Message = $"Ocorreu um problema ao tentar buscar as informações de categorias com filtros. Ex: {ex.Message}" };
            }
        }

    }
}
