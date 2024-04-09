import { useMutation } from "react-query";

import { queryClient } from "lib/react-query";
import { useApi } from "lib/api";
import storage from "utils/storage";

type DowngradeExtractorRequest = {
  nodeId: string;
  extractorId: string;
};

export const useDowngradeExtractor = () => {
  const api = useApi();
  const worldId = storage.getWorldId();

  return useMutation<string, unknown, DowngradeExtractorRequest>({
    onSuccess: () => {
      // Invalidating queries that show current extractor & extraction rates

      // Let these ones update behind the scenes since they're not as likely to be needed so fast
      queryClient.invalidateQueries("getResources"); // resource extraction rate totals
      queryClient.invalidateQueries("getWorldNodes"); // world node extraction rate

      // Wait until getWorldNodeDetails finishes updating before ending the mutation so that the world node details page updates
      return queryClient.invalidateQueries({
        queryKey: ["getWorldNodeDetails"],
      });
    },
    mutationFn: (variables: DowngradeExtractorRequest) => {
      return api.post(
        `/worlds/${worldId}/nodes/${variables.nodeId}/downgrade-extractor`,
        {
          extractorId: variables.extractorId,
        }
      );
    },
  });
};
