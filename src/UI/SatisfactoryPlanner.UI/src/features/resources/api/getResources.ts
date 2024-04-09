import { useQuery } from "react-query";

import storage from "utils/storage";
import { Resource } from "../types";
import { useApi } from "lib/api";

export const useGetResources = () => {
  const api = useApi();
  const worldId = storage.getWorldId();
  return useQuery<Resource[]>({
    queryKey: ["getResources"],
    queryFn: () => api.get(`/worlds/${worldId}/resources`),
  });
};
