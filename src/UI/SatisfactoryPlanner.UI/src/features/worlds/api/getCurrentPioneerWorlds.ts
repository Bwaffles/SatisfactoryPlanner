import { useQuery } from "react-query";

import { CurrentPioneerWorld } from "../types";
import { ApiClient, useApi } from "lib/api";

export const getCurrentPioneerWorlds = async (
  api: ApiClient
): Promise<CurrentPioneerWorld[]> => {
  return api.get("/worlds/worlds/@me");
};

export const useCurrentPioneerWorlds = () => {
  const api = useApi();
  return useQuery({
    queryKey: ["getCurrentPioneersWorlds"],
    queryFn: () => getCurrentPioneerWorlds(api),
  });
};
