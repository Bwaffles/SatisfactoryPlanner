import { useAuth0 } from "@auth0/auth0-react";
import { useQuery } from "react-query";

import * as Config from "../../../config";
import storage from "../../../utils/storage";
import { WorldNode } from "../types";
import { axios } from "../../../lib/axios";

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
        queryFn: () =>
            getWorldNodes(getAccessTokenSilently, worldId, resourceId),
    });
};
