import { useQuery } from "@tanstack/react-query";

import { ResourceDetails } from "../types";
import { useApi } from "lib/api";
import { resourceKeys } from "./queryKeys";

export const useGetResourceDetails = (resourceId: string) => {
  const api = useApi();
  return useQuery<ResourceDetails>({
    queryKey: resourceKeys.detail(resourceId),
    queryFn: () => api.get(`/resources/resources/${resourceId}`),
  });
};
