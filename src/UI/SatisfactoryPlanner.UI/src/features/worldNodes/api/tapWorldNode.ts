import { useMutation } from "react-query";

import { useApi } from "lib/api";
import { queryClient } from "lib/react-query";
import useUser from "providers/user-provider";

type TapWorldNodeRequest = {
  nodeId: string;
  extractorId: string;
};

export const useTapWorldNode = () => {
  const api = useApi();
  const { world } = useUser();

  return useMutation<string, unknown, TapWorldNodeRequest>({
    onSuccess: () => {
      // Invalidating queries that show whether a node has been tapped or not

      // Let this one update behind the scenes since it's not as likely to be needed so fast
      queryClient.invalidateQueries("getWorldNodes");

      // Wait until getWorldNodeDetails finishes updating before ending the mutation so that the node details page updates
      return queryClient.invalidateQueries({
        queryKey: ["getWorldNodeDetails"],
      });
    },
    mutationFn: (variables) => {
      return api.post(`/worlds/${world?.id}/nodes/${variables.nodeId}/tap`, {
        extractorId: variables.extractorId,
      });
    },
  });
};
