using System.Threading.Tasks;

using eShop.Domain.Orders;

namespace eShop.Persistence.MongoDb.Repositories
{
    public class OrderRepository
    {
        public Task AddAsync(OrderAggregateRoot order)
        {
            return Task.CompletedTask;
        }
    }
}
