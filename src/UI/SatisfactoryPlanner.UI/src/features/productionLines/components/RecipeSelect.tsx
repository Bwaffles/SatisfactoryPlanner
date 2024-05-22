import React from "react";
import { useGetItemRecipes } from "../api/getItemRecipes";
import { Recipe, RecipeItem } from "../types";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "components/Elements/Card/Card";
import { Separator } from "components/Elements/Separator/Separator";
import { cn } from "utils";

interface RecipeSelectProps {
  itemId: string;
}
export const RecipeSelect = (props: RecipeSelectProps) => {
  var { data: response } = useGetItemRecipes({ itemId: props.itemId });

  return (
    <div className="grid grid-cols-1 lg:grid-cols-2 gap-12">
      {RenderRecipeSection(
        props.itemId,
        `Recipes Producing ${props.itemId}`,
        response!.data.productRecipes
      )}
      {RenderRecipeSection(
        props.itemId,
        `Recipes Consuming ${props.itemId}`,
        response!.data.ingredientRecipes
      )}
    </div>
  );
};

function RenderRecipeSection(itemId: string, title: string, recipes: Recipe[]) {
  return (
    <div>
      <div className="text-xl font-semibold mb-3">{title}</div>
      <div className="flex flex-col gap-4">
        {recipes.map((recipe) => {
          return RenderRecipe(itemId, recipe);
        })}
      </div>
    </div>
  );
}

function RenderRecipe(itemId: string, recipe: Recipe): JSX.Element {
  return (
    <Card key={recipe.id}>
      <CardHeader>
        <CardTitle>{recipe.name}</CardTitle>
        <CardDescription>{recipe.type}</CardDescription>
      </CardHeader>
      <CardContent>
        <div className="grid grid-cols-[minmax(0,_1fr)_auto_minmax(0,_1fr)] gap-6">
          {RenderItems(itemId, "Ingredients", recipe.ingredients)}
          <Separator orientation="vertical"></Separator>
          {RenderItems(itemId, "Products", recipe.products)}
        </div>
      </CardContent>
    </Card>
  );
}

function RenderItems(itemId: string, title: string, items: RecipeItem[]) {
  return (
    <div>
      <div className="text-muted-foreground mb-3">{title}</div>
      {items.map((item) => {
        return (
          <div
            key={item.itemId}
            className={cn(
              "mb-1 flex flex-row justify-between gap-2",
              item.itemId === itemId ? "font-bold" : ""
            )}
          >
            <span>{item.itemName}</span>
            <span className="text-muted-foreground whitespace-nowrap">
              {item.amount.amountPerCycle} ({item.amount.amountPerMinute}
              /min)
            </span>
          </div>
        );
      })}
    </div>
  );
}
