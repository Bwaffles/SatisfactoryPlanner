import React, { useState } from "react";

import { ItemSelect } from "./ItemSelect";
import { RecipeSelect } from "./RecipeSelect";

export const ProcessedItems = () => {
  const [selectedItem, setSelectedItem] = useState<string>();

  return (
    <>
      <ItemSelect
        onItemSelected={(itemId) => setSelectedItem(itemId)}
      ></ItemSelect>
      <br />
      {selectedItem && (
        <>
          <p className="mb-3">Select a recipe</p>
          <RecipeSelect itemId={selectedItem}></RecipeSelect>
        </>
      )}
    </>
  );
};
