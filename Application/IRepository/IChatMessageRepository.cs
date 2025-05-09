using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IChatMessageRepository<T>
    {
        Task SaveMessageAsync(T message);
        Task<List<T>> GetAllMessagesAsync();
        Task ClearHistory();
    }
}
