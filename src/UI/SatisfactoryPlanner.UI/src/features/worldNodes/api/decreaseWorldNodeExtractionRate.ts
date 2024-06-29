import { useMutation } from "react-query";

import { queryClient } from "lib/react-query";
import { useApi } from "lib/api";
import useUser from "providers/user-provider";

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
    onSuccess: () => {
      // Invalidating queries that show extraction rates

      // Let these ones update behind the scenes since they're not as likely to be needed so fast
      queryClient.invalidateQueries("getResources"); // resource extraction rate totals
      queryClient.invalidateQueries("getWorldNodes"); // world node extraction rate

      // Wait until getWorldNodeDetails finishes updating before ending the mutation so that the world node details page updates
      return queryClient.invalidateQueries({
        queryKey: ["getWorldNodeDetails"],
      });
    },
    mutationFn: (variables: DecreaseWorldNodeExtractionRateRequest) => {
      return api.post(
        `/worlds/${world?.id}/nodes/${variables.nodeId}/decrease-extraction-rate`,
        variables.data
      );
    },
  });
};
