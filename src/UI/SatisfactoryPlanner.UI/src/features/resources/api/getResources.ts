import { useQuery } from "react-query";

import { Resource } from "../types";
import { useApi } from "lib/api";
import useUser from "providers/user-provider";

export const useGetResources = () => {
  const api = useApi();
  const { world } = useUser();
  return useQuery<Resource[]>({
    queryKey: ["getResources"],
    queryFn: () => api.get(`/worlds/${world?.id}/resources`),
  });
};
