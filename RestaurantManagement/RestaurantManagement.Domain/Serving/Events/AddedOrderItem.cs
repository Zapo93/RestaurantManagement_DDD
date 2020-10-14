namespace RestaurantManagement.Domain.Serving.Events
{
    public class AddedOrderItem
    {
        private int RecipeId;
        private string? Note;

        public AddedOrderItem(int recipeId, string? note)
        {
            this.RecipeId = recipeId;
            this.Note = note;
        }
    }
}