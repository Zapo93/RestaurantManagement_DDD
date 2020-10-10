namespace RestaurantManagement.Application.Serving.Commands.CreateDish
{
    public class CreateDishOutputModel
    {
        public CreateDishOutputModel(int dishId) 
        {
            DishId = dishId;
        }

        public int DishId { get; }
    }
}