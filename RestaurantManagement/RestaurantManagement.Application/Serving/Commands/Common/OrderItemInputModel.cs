namespace RestaurantManagement.Application.Serving.Commands.Common
{
    public class OrderItemInputModel
    {
        public OrderItemInputModel() { }

        public OrderItemInputModel(int dishId, string? note = null!) 
        {
            this.DishId = dishId;
            this.Note = note;
        }

        public int DishId { get; }
        public string? Note { get; }
    }
}