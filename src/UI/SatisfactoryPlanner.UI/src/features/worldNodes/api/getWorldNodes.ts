import { useAuth0 } from "@auth0/auth0-react";
import { useQuery } from "react-query";

import * as Config from "config";
import storage from "utils/storage";
import { WorldNode } from "../types";
import { axios } from "lib/axios";

export const getWorldNodes = async (
  getAccessTokenSilently: any,
  worldId: any,
  resourceId: string
): Promise<WorldNode[]> => {
  const accessToken = await getAccessTokenSilently({
    audience: Config.API_URL,
  });

  return axios.get(`/worlds/${worldId}/nodes?resourceId=${resourceId}`, {
    headers: {
      Authorization: `Bearer ${accessToken}`,
    },
  });
};

export const useGetWorldNodes = (resourceId: string) => {
  const { getAccessTokenSilently } = useAuth0();
  const worldId = storage.getWorldId();
  return useQuery({
    queryKey: ["getWorldNodes"],
    queryFn: async () => {
      const data = await getWorldNodes(
        getAccessTokenSilently,
        worldId,
        resourceId
      );

      // When user first signs up, the population of world nodes might not be ready yet so try again until it is
      if (data.length === 0) {
        return Promise.reject(new Error("World nodes not ready yet."));
      }

      return data;
    },
  });
};
