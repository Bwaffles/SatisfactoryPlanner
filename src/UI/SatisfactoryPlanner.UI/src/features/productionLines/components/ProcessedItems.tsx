import React, { Suspense, useState } from "react";

import { ItemSelect } from "./ItemSelect";
import { RecipeSelect } from "./RecipeSelect";
import { ProcessedItemAmountCalculator } from "./ProcessedItemAmountCalculator";

export const ProcessedItems = () => {
  const [selectedItemId, setSelectedItemId] = useState<string | null>(null);
  const [selectedRecipeId, setSelectedRecipeId] = useState<string | null>(null);

  return (
    <>
      <p className="mb-3">Select an item</p>
      <ItemSelect
        onItemSelected={(itemId) => {
          setSelectedItemId(itemId);
          setSelectedRecipeId(null);
        }}
      />

      {selectedItemId && (
        <>
          <p className="mt-6 mb-3">Select a recipe</p>
          <Suspense fallback={<p>Loading recipes...</p>}>
            <RecipeSelect
              itemId={selectedItemId}
              onRecipeSelected={(recipeId) => setSelectedRecipeId(recipeId)}
            />
          </Suspense>
        </>
      )}

      {selectedRecipeId && (
        <>
          <br />
          <p className="mt-6 mb-3">Enter the amount being processed</p>
          <Suspense fallback={<p>Loading recipe details...</p>}>
            <ProcessedItemAmountCalculator recipeId={selectedRecipeId} />
          </Suspense>
        </>
      )}
    </>
  );
};
