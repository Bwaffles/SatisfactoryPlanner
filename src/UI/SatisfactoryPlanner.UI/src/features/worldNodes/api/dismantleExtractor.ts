import { useAuth0 } from "@auth0/auth0-react";
import { useMutation } from "react-query";

import * as Config from "config";
import { axios } from "lib/axios";
import { queryClient } from "lib/react-query";
import storage from "utils/storage";

const dismantleExtractor = async (
  getAccessTokenSilently: any,
  worldId: string,
  nodeId: string
): Promise<string> => {
  const baseUrl = Config.API_URL;
  const accessToken = await getAccessTokenSilently({
    audience: baseUrl,
  });

  return axios.post(
    `/worlds/${worldId}/nodes/${nodeId}/dismantle-extractor`,
    null,
    {
      headers: {
        Authorization: `Bearer ${accessToken}`,
      },
    }
  );
};

type DismantleExtractorRequest = {
  nodeId: string;
};

export const useDismantleExtractor = () => {
  const { getAccessTokenSilently } = useAuth0();
  const worldId = storage.getWorldId();

  return useMutation({
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
      return dismantleExtractor(
        getAccessTokenSilently,
        worldId,
        variables.nodeId
      );
    },
  });
};
