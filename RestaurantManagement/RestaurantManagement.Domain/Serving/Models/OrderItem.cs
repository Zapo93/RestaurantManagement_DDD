using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Common.Models;

namespace RestaurantManagement.Domain.Serving.Models
{
    public class OrderItem: Entity<int>
    {
        public OrderItem(Dish dish, string? note = null!) 
        {
            Dish = dish;
            Note = note;
        }

        private OrderItem() { }
        public Dish Dish { get; private set; }
        public string? Note { get; private set; }
    }
}
