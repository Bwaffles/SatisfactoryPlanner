import { useQuery } from "react-query";

import storage from "utils/storage";
import { ProductionLineDetails } from "../types";
import { useApi } from "lib/api";

export type GetProductionLineDetailsRequest = {
  productionLineId: string;
};

export const useGetProductionLineDetails = (
  request: GetProductionLineDetailsRequest
) => {
  const api = useApi();
  const worldId = storage.getWorldId();
  return useQuery<ProductionLineDetails>({
    queryKey: ["getProductionLines"],
    queryFn: async () =>
      api.get(
        `/worlds/${worldId}/production-lines/${request.productionLineId}`
      ),
  });
};
