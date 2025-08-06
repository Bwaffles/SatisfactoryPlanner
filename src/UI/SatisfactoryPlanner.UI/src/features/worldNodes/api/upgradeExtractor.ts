import { useMutation } from "@tanstack/react-query";

import { queryClient } from "lib/react-query";
import { useApi } from "lib/api";
import useUser from "providers/user-provider";
import { worldNodeKeys } from "./queryKeys";

type UpgradeExtractorRequest = {
  nodeId: string;
  extractorId: string;
};

export const useUpgradeExtractor = () => {
  const api = useApi();
  const { world } = useUser();

  return useMutation<string, unknown, UpgradeExtractorRequest>({
    onSuccess: (_data: string, variables: UpgradeExtractorRequest) => {
      // Wait until getWorldNodeDetails finishes updating before ending the mutation so that the world node details page updates
      return queryClient.invalidateQueries(
        {
          queryKey: worldNodeKeys.details(variables.nodeId),
        },
        { cancelRefetch: false }
      );
    },
    mutationFn: (variables: UpgradeExtractorRequest) => {
      return api.post(
        `/worlds/${world?.id}/nodes/${variables.nodeId}/upgrade-extractor`,
        {
          extractorId: variables.extractorId,
        }
      );
    },
  });
};
