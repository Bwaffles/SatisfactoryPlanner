import { useMutation } from "react-query";

import { useApi } from "lib/api";
import { queryClient } from "lib/react-query";
import storage from "utils/storage";

type DismantleExtractorRequest = {
  nodeId: string;
};

export const useDismantleExtractor = () => {
  const api = useApi();
  const worldId = storage.getWorldId();

  return useMutation<string, unknown, DismantleExtractorRequest>({
    onSuccess: () => {
      // Invalidating queries that show whether a node has been tapped or not

      // Let this one update behind the scenes since it's not as likely to be needed so fast
      queryClient.invalidateQueries("getWorldNodes");

      // Wait until getWorldNodeDetails finishes updating before ending the mutation so that the node details page updates
      return queryClient.invalidateQueries({
        queryKey: ["getWorldNodeDetails"],
      });
    },
    mutationFn: (variables: DismantleExtractorRequest) => {
      return api.post(
        `/worlds/${worldId}/nodes/${variables.nodeId}/dismantle-extractor`,
        null
      );
    },
  });
};
