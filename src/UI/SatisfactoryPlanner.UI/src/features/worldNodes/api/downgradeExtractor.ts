import { useAuth0 } from "@auth0/auth0-react";
import { useMutation } from "react-query";

import * as Config from "config";
import { queryClient } from "lib/react-query";
import { axios } from "lib/axios";
import storage from "utils/storage";

const downgradeExtractor = async (
  getAccessTokenSilently: any,
  worldId: string,
  nodeId: string,
  extractorId: string
): Promise<string> => {
  const baseUrl = Config.API_URL;
  const accessToken = await getAccessTokenSilently({
    audience: baseUrl,
  });

  return axios.post(
    `/worlds/${worldId}/nodes/${nodeId}/downgrade-extractor`,
    {
      extractorId,
    },
    {
      headers: {
        Authorization: `Bearer ${accessToken}`,
      },
    }
  );
};

type DowngradeExtractorRequest = {
  nodeId: string;
  extractorId: string;
};

export const useDowngradeExtractor = () => {
  const { getAccessTokenSilently } = useAuth0();
  const worldId = storage.getWorldId();

  return useMutation({
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
      return downgradeExtractor(
        getAccessTokenSilently,
        worldId,
        variables.nodeId,
        variables.extractorId
      );
    },
  });
};
