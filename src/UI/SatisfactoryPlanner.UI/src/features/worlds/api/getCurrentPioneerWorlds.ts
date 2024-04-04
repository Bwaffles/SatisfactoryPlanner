import { useAuth0 } from "@auth0/auth0-react";
import { useQuery } from "react-query";

import * as Config from "config";
import { CurrentPioneerWorld } from "../types";
import { axios } from "lib/axios";

export const getCurrentPioneerWorlds = async (
  getAccessTokenSilently: any
): Promise<CurrentPioneerWorld[]> => {
  const accessToken = await getAccessTokenSilently({
    audience: Config.API_URL,
  });

  return axios.get("/worlds/worlds/@me", {
    headers: {
      Authorization: `Bearer ${accessToken}`,
    },
  });
};

export const useCurrentPioneerWorlds = () => {
  const { getAccessTokenSilently } = useAuth0();
  return useQuery({
    queryKey: ["getCurrentPioneersWorlds"],
    queryFn: () => getCurrentPioneerWorlds(getAccessTokenSilently),
  });
};
