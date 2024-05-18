import { useQuery } from "react-query";

import { useApi } from "lib/api";
import { processedItemsKeys } from "./queryKeys";
import { Recipe } from "../types";

export type GetItemRecipesRequest = {
  itemId: string;
};

export type GetItemRecipesResponse = {
  data: {
    ingredientRecipes: Recipe[];
    productRecipes: Recipe[];
  };
};

export const useGetItemRecipes = (request: GetItemRecipesRequest) => {
  const api = useApi();
  return useQuery<GetItemRecipesResponse>({
    queryKey: processedItemsKeys.itemRecipes(request.itemId),
    queryFn: async () =>
      api.get(`processed-items/items-to-process/${request.itemId}/recipes`),
  });
};
