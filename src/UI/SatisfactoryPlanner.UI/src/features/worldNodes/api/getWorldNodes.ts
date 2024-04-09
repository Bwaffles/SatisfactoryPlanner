import { useQuery } from "react-query";

import storage from "utils/storage";
import { WorldNode } from "../types";
import { useApi } from "lib/api";

export const useGetWorldNodes = (resourceId: string) => {
  const api = useApi();
  const worldId = storage.getWorldId();
  return useQuery<WorldNode[]>({
    queryKey: ["getWorldNodes"],
    queryFn: async () =>
      api.get(`/worlds/${worldId}/nodes?resourceId=${resourceId}`),
  });
};
