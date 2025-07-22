import { useMutation } from "react-query";

import { queryClient } from "lib/react-query";
import { useApi } from "lib/api";
import useUser from "providers/user-provider";
import { worldNodeKeys } from "./queryKeys";

export type DecreaseWorldNodeExtractionRateRequest = {
  nodeId: string;
  data: {
    extractionRate: number;
  };
};

export const useDecreaseWorldNodeExtractionRate = () => {
  const api = useApi();
  const { world } = useUser();

  return useMutation<string, unknown, DecreaseWorldNodeExtractionRateRequest>({
    onSuccess: (
      _data: string,
      variables: DecreaseWorldNodeExtractionRateRequest
    ) => {
      // Wait until getWorldNodeDetails finishes updating before ending the mutation so that the world node details page updates
      return queryClient.invalidateQueries(
        {
          queryKey: worldNodeKeys.details(variables.nodeId),
        },
        { cancelRefetch: false }
      );
    },
    mutationFn: (variables: DecreaseWorldNodeExtractionRateRequest) => {
      return api.post(
        `/worlds/${world?.id}/nodes/${variables.nodeId}/decrease-extraction-rate`,
        variables.data
      );
    },
  });
};
