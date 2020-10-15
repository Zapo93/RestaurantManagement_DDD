using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantManagement.Application;
using RestaurantManagement.Application.Kitchen.Commands.CreateRecipe;
using RestaurantManagement.Application.Kitchen.Queries.GetRecipes;
using RestaurantManagement.Domain;
using RestaurantManagement.Domain.Kitchen.Models;
using RestaurantManagement.Infrastructure;
using RestaurantManagement.Web;
using RestaurantManagement.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManagement.Tests
{
    [TestClass]
    public class KitchenControllerTests
    {
        [TestMethod]
        public async Task CreateRecipe_NewRecipe_SuccessfullRead() 
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDomain()
                .AddApplication()
                //.AddInfrastructure()
                .AddWebComponents();

            var kitchenController = new KitchenController();

            var createRecipeCommand = new CreateRecipeCommand();

            createRecipeCommand.Name = "Mandja";
            createRecipeCommand.Description = "Vkusno";
            createRecipeCommand.Preparation = "Gotvish";

            createRecipeCommand.Ingredients.Add(new Ingredient("Qdene", 500));

            var createRecipeOutput = await kitchenController.CreateRecipe(createRecipeCommand);

            var recipiesQuery = new GetRecipesQuery();
            recipiesQuery.OnlyActive = true;

            var recipesResult = await kitchenController.GetRecipiesQuery(recipiesQuery);
            var recipe = recipesResult.Value.Recipes.FirstOrDefault(recipe => recipe.Id == createRecipeOutput.Value.RecipeId);

            Assert.AreEqual(createRecipeCommand.Name, recipe.Name);
            Assert.AreEqual(createRecipeCommand.Description, recipe.Description);
            Assert.AreEqual(createRecipeCommand.Preparation, recipe.Preparation);
        }
    }
}
