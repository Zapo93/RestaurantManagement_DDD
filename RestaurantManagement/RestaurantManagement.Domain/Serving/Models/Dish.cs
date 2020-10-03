using RestaurantManagement.Domain.Common;

namespace RestaurantManagement.Domain.Serving.Models
{
    public class Dish : Entity<int>, IAggregateRoot
    {
        internal Dish(int recipeId, string description, Money price, Uri imageUrl = null!) 
        {
            RecipeId = recipeId;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
        }

        public int RecipeId { get; private set; }
        public string Description { get; private set; }
        public Money Price { get; private set; }
        public Uri? ImageUrl { get; private set; }
    }
}
