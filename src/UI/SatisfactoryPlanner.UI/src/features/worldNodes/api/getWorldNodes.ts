import { useQuery } from "react-query";

import { WorldNode } from "../types";
import { useApi } from "lib/api";
import useUser from "providers/user-provider";

export const useGetWorldNodes = (resourceId: string) => {
  const api = useApi();
  const { world } = useUser();
  return useQuery<GetWorldNodesReponse>({
    queryKey: ["getWorldNodes"],
    queryFn: async () =>
      api.get(`/worlds/${world?.id}/nodes?resourceId=${resourceId}`),
  });
};

export type GetWorldNodesReponse = {
  data: GetWorldNodeResult;
};

export type GetWorldNodeResult = {
  worldNodes: WorldNode[];
};
