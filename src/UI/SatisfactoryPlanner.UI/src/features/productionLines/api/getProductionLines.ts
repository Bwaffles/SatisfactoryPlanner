import { useAuth0 } from "@auth0/auth0-react";
import { useQuery } from "react-query";

import * as Config from "config";
import storage from "utils/storage";
import { ProductionLine } from "../types";
import { axios } from "lib/axios";

export const getProductionLines = async (
  getAccessTokenSilently: any,
  worldId: any
): Promise<ProductionLine[]> => {
  const accessToken = await getAccessTokenSilently({
    audience: Config.API_URL,
  });

  return axios.get(`/worlds/${worldId}/production-lines`, {
    headers: {
      Authorization: `Bearer ${accessToken}`,
    },
  });
};

export const useGetProductionLines = () => {
  const { getAccessTokenSilently } = useAuth0();
  const worldId = storage.getWorldId();
  return useQuery({
    queryKey: ["getProductionLines"],
    queryFn: async () => getProductionLines(getAccessTokenSilently, worldId),
  });
};
