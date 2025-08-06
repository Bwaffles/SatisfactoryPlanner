import React from "react";
import { useGetRecipeDetails } from "../api/getRecipeDetails";

interface ProcessedItemAmountCalculatorProps {
  recipeId: string;
}

export const ProcessedItemAmountCalculator = (
  props: ProcessedItemAmountCalculatorProps
) => {
  var { data: response } = useGetRecipeDetails({ recipeId: props.recipeId });
  var recipeDetails = response!.data;

  return (
    <>
      <p>
        Selected {recipeDetails.name} ({recipeDetails.type}).
      </p>
      <br />
      <b>Ingredients</b>
      {response?.data.ingredients.map((ingredient) => {
        return (
          <div key={ingredient.itemId}>
            <p>
              {ingredient.itemName}: {ingredient.amount.amountPerCycle} (
              {ingredient.amount.amountPerMinute}/min)
            </p>
          </div>
        );
      })}
      <br />
      <b>Products</b>
      {response?.data.products.map((product) => {
        return (
          <div key={product.itemId}>
            <p>
              {product.itemName}: {product.amount.amountPerCycle} (
              {product.amount.amountPerMinute}/min)
            </p>
          </div>
        );
      })}
      <br />
      <p>Produced In</p>
      {recipeDetails.producedIn.map((building) => {
        return (
          <div key={building.id}>
            <p>{building.name}</p>
            <p>Clockspeed: 100%</p>
          </div>
        );
      })}
    </>
  );
};
