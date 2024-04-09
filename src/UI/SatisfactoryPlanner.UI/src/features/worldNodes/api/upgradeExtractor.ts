import { useMutation } from "react-query";

import { queryClient } from "lib/react-query";
import { useApi } from "lib/api";
import storage from "utils/storage";

type UpgradeExtractorRequest = {
  nodeId: string;
  extractorId: string;
};

export const useUpgradeExtractor = () => {
  const api = useApi();
  const worldId = storage.getWorldId();

  return useMutation<string, unknown, UpgradeExtractorRequest>({
    onSuccess: () => {
      // Invalidating queries that show current extractor

      // Wait until getWorldNodeDetails finishes updating before ending the mutation so that the world node details page updates
      return queryClient.invalidateQueries({
        queryKey: ["getWorldNodeDetails"],
      });
    },
    mutationFn: (variables: UpgradeExtractorRequest) => {
      return api.post(
        `/worlds/${worldId}/nodes/${variables.nodeId}/upgrade-extractor`,
        {
          extractorId: variables.extractorId,
        }
      );
    },
  });
};
