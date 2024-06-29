import { useQuery } from "react-query";

import { ProductionLineDetails } from "../types";
import { useApi } from "lib/api";
import { productionLineKeys } from "./queryKeys";
import useUser from "providers/user-provider";

export type GetProductionLineDetailsRequest = {
  productionLineId: string;
};

export const useGetProductionLineDetails = (
  request: GetProductionLineDetailsRequest
) => {
  const api = useApi();
  const { world } = useUser();
  return useQuery<ProductionLineDetails>({
    queryKey: productionLineKeys.detail(request.productionLineId),
    queryFn: async () =>
      api.get(
        `/worlds/${world?.id}/production-lines/${request.productionLineId}`
      ),
  });
};
