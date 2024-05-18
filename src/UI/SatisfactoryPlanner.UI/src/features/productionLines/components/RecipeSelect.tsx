import React from "react";
import { useGetItemRecipes } from "../api/getItemRecipes";
import { Recipe } from "../types";

interface RecipeSelectProps {
  itemId: string;
}
export const RecipeSelect = (props: RecipeSelectProps) => {
  var { data: response } = useGetItemRecipes({ itemId: props.itemId });

  return (
    <>
      <div className="flex gap-6 justify-between">
        <div>
          <h3 className="scroll-m-20 text-2xl font-semibold tracking-tight mb-3">
            Ingredient Recipes
          </h3>
          {response?.data.ingredientRecipes.map((recipe) => {
            return RenderRecipe(recipe);
          })}
        </div>
        <div>
          <h3 className="scroll-m-20 text-2xl font-semibold tracking-tight mb-3">
            Produced Recipes
          </h3>
          {response?.data.productRecipes.map((recipe) => {
            return RenderRecipe(recipe);
          })}
        </div>
      </div>
    </>
  );
};

function RenderRecipe(recipe: Recipe): JSX.Element {
  return (
    <div key={recipe.id}>
      <div className="font-bold text-lg">
        {recipe.name} - {recipe.type}
      </div>
      <div className="flex gap-12">
        <div>
          <p className="font-semibold">Ingredients</p>
          {recipe.ingredients.map((ingredient) => {
            return (
              <div key={ingredient.itemId} className="mb-3">
                {ingredient.amount.amountPerCycle} (
                {ingredient.amount.amountPerMinute}/min) - {ingredient.itemName}
              </div>
            );
          })}
        </div>
        <div>
          <p className="font-semibold">Products</p>
          {recipe.products.map((product) => {
            return (
              <div key={product.itemId} className="mb-3">
                {product.amount.amountPerCycle} (
                {product.amount.amountPerMinute}/min) - {product.itemName}
              </div>
            );
          })}
        </div>
      </div>
    </div>
  );
}
