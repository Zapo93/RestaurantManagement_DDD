using RestaurantManagement.Common.Domain.Models;

namespace RestaurantManagement.Serving.Domain.Models
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
