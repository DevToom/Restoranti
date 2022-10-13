using Domain.Interface;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IRProduct _rProduct;

        public ProductService(IRProduct rProduct)
        {
            this._rProduct = rProduct;
        }

        public async Task<MessageResponse<Product>> Add(Product product)
        {
            try
            {
                if (product != null)
                {
                    await _rProduct.AddAsync(product);
                    return new MessageResponse<Product> { Entity = product, Message = "Produto adicionada com sucesso!" };
                }
                else
                {
                    return new MessageResponse<Product> { HasError = true, Message = "Não há nenhuma informação sobre o novo produto. (null)" };
                }
            }
            catch (Exception ex)
            {
                return new MessageResponse<Product> { HasError = true, Message = $"Não foi possível adicionar  o novo produto - {ex.Message}" };
            }

        }

        public async Task<MessageResponse<Product>> Update(Product product)
        {
            try
            {
                if (product != null)
                {
                    var existentProduct = await _rProduct.GetById(product.ProductId);
                    if (existentProduct != null)
                    {
                        existentProduct.CategoryId = product.CategoryId;
                        existentProduct.Name = product.Name;
                        existentProduct.Description = product.Description;
                        existentProduct.PriceALaCarte = product.PriceALaCarte;
                        existentProduct.PriceRodizio = product.PriceRodizio;
                        existentProduct.Status = product.Status;
                        existentProduct.ImageContent = product.ImageContent;
                        existentProduct.ModifiedDate = product.ModifiedDate;
                        existentProduct.ModifiedUserId = product.ModifiedUserId;

                        await _rProduct.Update(existentProduct);

                        return new MessageResponse<Product> { Entity = product, Message = "Produto atualizado com sucesso!" };
                    }
                    else
                        return new MessageResponse<Product> { Entity = product, Message = "Produto não encontrado para ser atualizado!" };
                }
                else
                {
                    return new MessageResponse<Product> { HasError = true, Message = "Não há nenhuma informação sobre o produto para ser atualizado. (null)" };
                }
            }
            catch (Exception ex)
            {
                return new MessageResponse<Product> { HasError = true, Message = $"Não foi possível editar o produto - {ex.Message}" };
            }

        }

        public async Task<MessageResponse<Product>> DeleteProduct(int ProductId)
        {
            try
            {
                if (ProductId != 0)
                {
                    var product = await _rProduct.GetById(ProductId);
                    if (product != null)
                    {
                        await _rProduct.Delete(product);

                        return new MessageResponse<Product> { Entity = product, Message = "Produto removido com sucesso!" };
                    }
                    else
                        return new MessageResponse<Product> { Entity = product, Message = "Produto não encontrado para ser removido!" };
                }
                else
                {
                    return new MessageResponse<Product> { HasError = true, Message = $"Não há nenhum produto com o Id {ProductId}!" };
                }
            }
            catch (Exception ex)
            {
                return new MessageResponse<Product> { HasError = true, Message = $"Não foi possível excluir a categoria - {ex.Message}" };
            }

        }

        public async Task<MessageResponse<List<Product>>> GetList()
        {
            try
            {
                var entityList = await _rProduct.GetList();
                return new MessageResponse<List<Product>> { Entity = entityList };
            }
            catch (Exception ex)
            {
                return new MessageResponse<List<Product>> { HasError = true, Message = $"Ocorreu um problema ao tentar buscar as informações de produtos. Ex: {ex.Message}" };
            }
        }

        public async Task<MessageResponse<List<Product>>> GetListByFilters(string Name, int CategoryId, int Status)
        {
            try
            {
                var entityList = await _rProduct.GetList();
                entityList = entityList.Where(x => x.Name.ToUpper().Contains(Name.ToUpper()) && x.CategoryId == CategoryId && x.Status == (EProductStatus)Status).ToList();
                entityList = entityList.OrderBy(x => x.CategoryId).ToList();

                return new MessageResponse<List<Product>> { Entity = entityList };
            }
            catch (Exception ex)
            {
                return new MessageResponse<List<Product>> { HasError = true, Message = $"Ocorreu um problema ao tentar buscar as informações de produtos com filtros. Ex: {ex.Message}" };
            }
        }



    }
}
