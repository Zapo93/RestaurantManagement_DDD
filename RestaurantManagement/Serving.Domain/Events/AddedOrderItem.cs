namespace RestaurantManagement.Serving.Domain.Events
{
    public class AddedOrderItem
    {
        public readonly int RecipeId;
        public readonly string? Note;

        public AddedOrderItem(int recipeId, string? note)
        {
            this.RecipeId = recipeId;
            this.Note = note;
        }
    }
}