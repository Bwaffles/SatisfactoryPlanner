import { useQuery } from "@tanstack/react-query";

import { useApi } from "lib/api";
import { processedItemsKeys } from "./queryKeys";
import { RecipeDetails } from "../types";

export type GetRecipeDetailsRequest = {
  recipeId: string;
};

export type GetRecipeDetailsResponse = {
  data: RecipeDetails;
};

export const useGetRecipeDetails = (request: GetRecipeDetailsRequest) => {
  const api = useApi();
  return useQuery<GetRecipeDetailsResponse>({
    queryKey: processedItemsKeys.recipeDetails(request.recipeId),
    queryFn: async () => api.get(`processed-items/recipes/${request.recipeId}`),
  });
};
