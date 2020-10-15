﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Application.Kitchen.Commands.CreateRequest
{
    public class RequestItemInputModel
    {
        public readonly int RecipeId;
        public readonly string Note;

        public RequestItemInputModel() {}

        public RequestItemInputModel(int recipeId, string? note)
        {
            RecipeId = recipeId;
            Note = note;
        }
    }
}
