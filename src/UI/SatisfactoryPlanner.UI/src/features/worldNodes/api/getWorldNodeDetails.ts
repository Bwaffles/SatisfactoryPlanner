import { useQuery } from "react-query";

import { useApi } from "lib/api";
import useUser from "providers/user-provider";

export const useGetWorldNodeDetails = (nodeId: string) => {
  const api = useApi();
  const { world } = useUser();

  return useQuery<GetWorldNodeDetailsResponse>({
    queryKey: ["getWorldNodeDetails"],
    queryFn: () => api.get(`/worlds/${world?.id}/nodes/${nodeId}`),
  });
};

export type GetWorldNodeDetailsResponse = {
  data: GetWorldNodeDetailsResult;
};

export type GetWorldNodeDetailsResult = {
  details: WorldNodeDetails;
};

export type WorldNodeDetails = {
  nodeId: string;
  nodeName: string;
  purity: string;
  biome: string;
  number: number;
  resourceId: string;
  resourceName: string;
  isTapped: boolean;
  extractorId: string | null;
  extractionRate: number;
  availableExtractors: AvailableExtractor[];
};

export type AvailableExtractor = {
  id: string;
  name: string;
  maxExtractionRate: number;
};
