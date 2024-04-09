import { useQuery } from "react-query";

import storage from "utils/storage";
import { ProductionLine } from "../types";
import { useApi } from "lib/api";

export const useGetProductionLines = () => {
  const api = useApi();
  const worldId = storage.getWorldId();
  return useQuery<ProductionLine[]>({
    queryKey: ["getProductionLines"],
    queryFn: async () => api.get(`/worlds/${worldId}/production-lines`),
  });
};
