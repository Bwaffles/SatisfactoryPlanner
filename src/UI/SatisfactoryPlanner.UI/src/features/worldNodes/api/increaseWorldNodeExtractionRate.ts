import { useMutation } from "@tanstack/react-query";

import { queryClient } from "lib/react-query";
import { useApi } from "lib/api";
import useUser from "providers/user-provider";
import { worldNodeKeys } from "./queryKeys";

export type IncreaseWorldNodeExtractionRateRequest = {
  nodeId: string;
  data: {
    extractionRate: number;
  };
};

export const useIncreaseWorldNodeExtractionRate = () => {
  const api = useApi();
  const { world } = useUser();

  return useMutation<string, unknown, IncreaseWorldNodeExtractionRateRequest>({
    onSuccess: (
      _data: string,
      variables: IncreaseWorldNodeExtractionRateRequest
    ) => {
      // Wait until getWorldNodeDetails finishes updating before ending the mutation so that the world node details page updates
      return queryClient.invalidateQueries(
        {
          queryKey: worldNodeKeys.details(variables.nodeId),
        },
        { cancelRefetch: false }
      );
    },
    mutationFn: (variables: IncreaseWorldNodeExtractionRateRequest) => {
      return api.post(
        `/worlds/${world?.id}/nodes/${variables.nodeId}/increase-extraction-rate`,
        variables.data
      );
    },
  });
};
