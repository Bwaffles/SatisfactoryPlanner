import { useMutation } from "react-query";

import { queryClient } from "lib/react-query";
import { useApi } from "lib/api";
import useUser from "providers/user-provider";
import { worldNodeKeys } from "./queryKeys";

type DowngradeExtractorRequest = {
  nodeId: string;
  extractorId: string;
};

export const useDowngradeExtractor = () => {
  const api = useApi();
  const { world } = useUser();

  return useMutation<string, unknown, DowngradeExtractorRequest>({
    onSuccess: (_data: string, variables: DowngradeExtractorRequest) => {
      // Wait until getWorldNodeDetails finishes updating before ending the mutation so that the world node details page updates
      return queryClient.invalidateQueries(
        {
          queryKey: worldNodeKeys.details(variables.nodeId),
        },
        { cancelRefetch: false }
      );
    },
    mutationFn: (variables: DowngradeExtractorRequest) => {
      return api.post(
        `/worlds/${world?.id}/nodes/${variables.nodeId}/downgrade-extractor`,
        {
          extractorId: variables.extractorId,
        }
      );
    },
  });
};
