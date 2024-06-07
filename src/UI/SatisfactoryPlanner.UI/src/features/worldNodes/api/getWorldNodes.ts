import { useQuery } from "react-query";

import storage from "utils/storage";
import { WorldNode } from "../types";
import { useApi } from "lib/api";

export const useGetWorldNodes = (resourceId: string) => {
  const api = useApi();
  const worldId = storage.getWorldId();
  return useQuery<GetWorldNodesReponse>({
    queryKey: ["getWorldNodes"],
    queryFn: async () =>
      api.get(`/worlds/${worldId}/nodes?resourceId=${resourceId}`),
  });
};

export type GetWorldNodesReponse = {
  data: GetWorldNodeResult;
};

export type GetWorldNodeResult = {
  worldNodes: WorldNode[];
};
