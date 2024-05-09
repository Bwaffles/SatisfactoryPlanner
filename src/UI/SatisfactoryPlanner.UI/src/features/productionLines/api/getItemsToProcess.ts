import { useQuery } from "react-query";

import { useApi } from "lib/api";
import { processedItemsKeys } from "./queryKeys";
import { ItemToProcess } from "../types";

export type GetItemsToProcessResponse = {
  items: ItemToProcess[];
};

export const useGetItemsToProcess = () => {
  const api = useApi();
  return useQuery<GetItemsToProcessResponse>({
    queryKey: processedItemsKeys.itemsToProcess(),
    queryFn: async () => api.get(`processed-items/items-to-process`),
  });
};
