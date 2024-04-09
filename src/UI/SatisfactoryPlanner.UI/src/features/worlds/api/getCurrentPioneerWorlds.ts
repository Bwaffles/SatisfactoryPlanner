import { useQuery } from "react-query";

import { CurrentPioneerWorld } from "../types";
import { useApi } from "lib/api";
import { AxiosInstance } from "axios";

export const getCurrentPioneerWorlds = async (
  api: AxiosInstance
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
