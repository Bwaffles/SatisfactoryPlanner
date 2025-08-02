import React, { useState } from "react";
import { useGetItemRecipes } from "../api/getItemRecipes";
import { Recipe, RecipeItem } from "../types";
import {
  Card,
  CardActionArea,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "components/Elements/Card/Card";
import { Separator } from "components/Elements/Separator/Separator";
import { cn } from "utils";

interface RecipeSelectProps {
  itemId: string;
  onRecipeSelected?(recipeId: string): void;
}
export const RecipeSelect = (props: RecipeSelectProps) => {
  var { data: response } = useGetItemRecipes({ itemId: props.itemId });
  const [selectedRecipeId, setSelectedRecipeId] = useState<string | null>(null);

  const RenderRecipeSection = (title: string, recipes: Recipe[]) => {
    return (
      <div>
        <div className="text-xl font-semibold mb-3">{title}</div>
        <div className="flex flex-col gap-4">
          {recipes.map((recipe) => {
            return (
              <Card key={recipe.id}>
                <CardActionArea
                  onClick={() => {
                    if (props.onRecipeSelected)
                      props.onRecipeSelected(recipe.id);
                    setSelectedRecipeId(recipe.id);
                  }}
                  isSelected={selectedRecipeId == recipe.id}
                >
                  <CardHeader>
                    <CardTitle>{recipe.name}</CardTitle>
                    <CardDescription>{recipe.type}</CardDescription>
                  </CardHeader>
                  <CardContent>
                    <div className="grid grid-cols-[minmax(0,_1fr)_auto_minmax(0,_1fr)] gap-6">
                      {RenderItems(
                        props.itemId,
                        "Ingredients",
                        recipe.ingredients
                      )}
                      <Separator orientation="vertical"></Separator>
                      {RenderItems(props.itemId, "Products", recipe.products)}
                    </div>
                  </CardContent>
                </CardActionArea>
              </Card>
            );
          })}
        </div>
      </div>
    );
  };

  return (
    <div className="grid grid-cols-1 lg:grid-cols-2 gap-12">
      {RenderRecipeSection(
        `Recipes Producing ${props.itemId}`,
        response!.data.productRecipes
      )}
      {RenderRecipeSection(
        `Recipes Consuming ${props.itemId}`,
        response!.data.ingredientRecipes
      )}
    </div>
  );
};

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
