import { useAuth0 } from "@auth0/auth0-react";
import { useMutation } from "react-query";

import * as Config from "config";
import { queryClient } from "lib/react-query";
import storage from "utils/storage";
import { axios } from "lib/axios";

const setUpProductionLine = async (
  getAccessTokenSilently: any,
  worldId: string,
  request: SetUpProductionLineRequest
): Promise<string> => {
  const accessToken = await getAccessTokenSilently({
    audience: Config.API_URL,
  });

  return axios.post(
    `/worlds/${worldId}/production-lines/set-up`,
    request.data,
    {
      headers: {
        Authorization: `Bearer ${accessToken}`,
      },
    }
  );
};

export type SetUpProductionLineRequest = {
  data: {
    name: string;
  };
};

export const useSetUpProductionLine = () => {
  const { getAccessTokenSilently } = useAuth0();
  const worldId = storage.getWorldId();

  return useMutation({
    onSuccess: () => {
      queryClient.invalidateQueries("getProductionLines");
    },
    mutationFn: (variables: SetUpProductionLineRequest) => {
      return setUpProductionLine(getAccessTokenSilently, worldId, variables);
    },
  });
};
