import { useQuery } from "react-query";

import storage from "utils/storage";
import { useApi } from "lib/api";

export const useGetWorldNodeDetails = (nodeId: string) => {
  const api = useApi();
  const worldId = storage.getWorldId();

  return useQuery<GetWorldNodeDetailsResponse>({
    queryKey: ["getWorldNodeDetails"],
    queryFn: () => api.get(`/worlds/${worldId}/nodes/${nodeId}`),
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
