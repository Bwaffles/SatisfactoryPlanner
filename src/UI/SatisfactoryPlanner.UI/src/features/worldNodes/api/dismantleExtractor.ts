import { useMutation } from "@tanstack/react-query";

import { useApi } from "lib/api";
import { queryClient } from "lib/react-query";
import useUser from "providers/user-provider";
import { worldNodeKeys } from "./queryKeys";

type DismantleExtractorRequest = {
  nodeId: string;
};

export const useDismantleExtractor = () => {
  const api = useApi();
  const { world } = useUser();

  return useMutation<string, unknown, DismantleExtractorRequest>({
    onSuccess: (_data: string, variables: DismantleExtractorRequest) => {
      // Wait until getWorldNodeDetails finishes updating before ending the mutation so that the node details page updates
      return queryClient.invalidateQueries(
        {
          queryKey: worldNodeKeys.details(variables.nodeId),
        },
        { cancelRefetch: false }
      );
    },
    mutationFn: (variables: DismantleExtractorRequest) => {
      return api.post(
        `/worlds/${world?.id}/nodes/${variables.nodeId}/dismantle-extractor`,
        null
      );
    },
  });
};
