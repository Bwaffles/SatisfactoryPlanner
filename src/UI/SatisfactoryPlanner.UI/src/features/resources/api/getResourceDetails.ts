import { useQuery } from "react-query";

import { ResourceDetails } from "../types";
import { useApi } from "lib/api";

export const useGetResourceDetails = (resourceId: string) => {
  const api = useApi();
  return useQuery<ResourceDetails>({
    queryKey: ["getResourceDetails"],
    queryFn: () => api.get(`/resources/resources/${resourceId}`),
  });
};
