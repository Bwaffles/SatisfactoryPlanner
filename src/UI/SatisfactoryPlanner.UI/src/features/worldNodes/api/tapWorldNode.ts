import { useMutation } from "@tanstack/react-query";

import { useApi } from "lib/api";
import { queryClient } from "lib/react-query";
import useUser from "providers/user-provider";
import { worldNodeKeys } from "./queryKeys";

type TapWorldNodeRequest = {
  nodeId: string;
  extractorId: string;
};

export const useTapWorldNode = () => {
  const api = useApi();
  const { world } = useUser();

  return useMutation<string, unknown, TapWorldNodeRequest>({
    onSuccess: (_data: string, variables: TapWorldNodeRequest) => {
      // Wait until getWorldNodeDetails finishes updating before ending the mutation so that the node details page updates
      return queryClient.invalidateQueries(
        {
          queryKey: worldNodeKeys.details(variables.nodeId),
        },
        { cancelRefetch: false }
      );
    },
    mutationFn: (variables) => {
      return api.post(`/worlds/${world?.id}/nodes/${variables.nodeId}/tap`, {
        extractorId: variables.extractorId,
      });
    },
  });
};
