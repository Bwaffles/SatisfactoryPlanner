import { useAuth0 } from "@auth0/auth0-react";
import { useQuery } from "react-query";

import * as Config from "../../../config";
import storage from "../../../utils/storage";
import { WorldNodeDetails } from "../types";
import { axios } from "../../../lib/axios";

export const getWorldNodeDetails = async (
    getAccessTokenSilently: any,
    worldId: string,
    nodeId: string
): Promise<WorldNodeDetails> => {
    const accessToken = await getAccessTokenSilently({
        audience: Config.API_URL,
    });

    return axios.get(`/worlds/${worldId}/nodes/${nodeId}`, {
        headers: {
            Authorization: `Bearer ${accessToken}`,
        },
    });
};

export const useGetWorldNodeDetails = (nodeId: string) => {
    const { getAccessTokenSilently } = useAuth0();
    const worldId = storage.getWorldId();

    return useQuery({
        queryKey: ["getWorldNodeDetails"],
        queryFn: () =>
            getWorldNodeDetails(getAccessTokenSilently, worldId, nodeId),
    });
};
