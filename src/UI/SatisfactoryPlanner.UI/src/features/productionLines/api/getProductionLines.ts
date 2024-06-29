import { useQuery } from "react-query";

import { ProductionLine } from "../types";
import { useApi } from "lib/api";
import { productionLineKeys } from "./queryKeys";
import useUser from "providers/user-provider";

export const useGetProductionLines = () => {
  const api = useApi();
  const { world } = useUser();
  return useQuery<ProductionLine[]>({
    queryKey: productionLineKeys.lists(),
    queryFn: async () => api.get(`/worlds/${world?.id}/production-lines`),
  });
};
