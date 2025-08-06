import { useQuery } from "@tanstack/react-query";

import { Resource } from "../types";
import { useApi } from "lib/api";
import useUser from "providers/user-provider";
import { resourceKeys } from "./queryKeys";

export const useGetResources = () => {
  const api = useApi();
  const { world } = useUser();
  return useQuery<Resource[]>({
    queryKey: resourceKeys.list(),
    queryFn: () => api.get(`/worlds/${world?.id}/resources`),
  });
};
