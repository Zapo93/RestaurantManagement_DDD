namespace RestaurantManagement.Serving.Application.Commands.CreateDish
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