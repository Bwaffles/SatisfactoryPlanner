import { useAuth0 } from "@auth0/auth0-react";
import { useMutation } from "react-query";

import * as Config from "config";
import { queryClient } from "lib/react-query";
import storage from "utils/storage";
import { axios } from "lib/axios";

const increaseWorldNodeExtractionRate = async (
  getAccessTokenSilently: any,
  worldId: string,
  request: IncreaseWorldNodeExtractionRateRequest
): Promise<string> => {
  const accessToken = await getAccessTokenSilently({
    audience: Config.API_URL,
  });

  return axios.post(
    `/worlds/${worldId}/nodes/${request.nodeId}/increase-extraction-rate`,
    request.data,
    {
      headers: {
        Authorization: `Bearer ${accessToken}`,
      },
    }
  );
};

export type IncreaseWorldNodeExtractionRateRequest = {
  nodeId: string;
  data: {
    extractionRate: number;
  };
};

export const useIncreaseWorldNodeExtractionRate = () => {
  const { getAccessTokenSilently } = useAuth0();
  const worldId = storage.getWorldId();

  return useMutation({
    onSuccess: () => {
      // Invalidating queries that show extraction rates

      // Let these ones update behind the scenes since they're not as likely to be needed so fast
      queryClient.invalidateQueries("getResources"); // resource extraction rate totals
      queryClient.invalidateQueries("getWorldNodes"); // world node extraction rate

      // Wait until getWorldNodeDetails finishes updating before ending the mutation so that the world node details page updates
      return queryClient.invalidateQueries({
        queryKey: ["getWorldNodeDetails"],
      });
    },
    mutationFn: (variables: IncreaseWorldNodeExtractionRateRequest) => {
      return increaseWorldNodeExtractionRate(
        getAccessTokenSilently,
        worldId,
        variables
      );
    },
  });
};
