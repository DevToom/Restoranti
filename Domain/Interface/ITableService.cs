using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface ITableService
    {
        Task<MessageResponse<Table>> Add(Table request);
        Task<MessageResponse<Table>> Delete(int TableId);
        Task<MessageResponse<List<Table>>> GetList();
        Task<bool> UpdateStatusTable(int TableNumber);
        Task<MessageResponse<List<Table>>> GetListByFilters(int TableNumber, string Status);

    }
}
