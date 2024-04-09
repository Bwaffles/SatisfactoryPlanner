import { useQuery } from "react-query";

import storage from "utils/storage";
import { WorldNodeDetails } from "../types";
import { useApi } from "lib/api";

export const useGetWorldNodeDetails = (nodeId: string) => {
  const api = useApi();
  const worldId = storage.getWorldId();

  return useQuery<WorldNodeDetails>({
    queryKey: ["getWorldNodeDetails"],
    queryFn: () => api.get(`/worlds/${worldId}/nodes/${nodeId}`),
  });
};
