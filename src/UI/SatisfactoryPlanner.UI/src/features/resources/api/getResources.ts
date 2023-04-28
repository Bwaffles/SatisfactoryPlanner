import { useAuth0 } from "@auth0/auth0-react";
import { useQuery } from "react-query";

import * as Config from "../../../config";
import storage from "../../../utils/storage";
import { Resource } from "../types";
import { axios } from "../../../lib/axios";

export const getResources = async (
    getAccessTokenSilently: any,
    worldId: string
): Promise<Resource[]> => {
    const accessToken = await getAccessTokenSilently({
        audience: Config.API_URL,
    });
    return axios.get(`/worlds/${worldId}/resources`, {
        headers: {
            Authorization: `Bearer ${accessToken}`,
        },
    });
};

export const useGetResources = () => {
    const { getAccessTokenSilently } = useAuth0();
    const worldId = storage.getWorldId();
    return useQuery({
        queryKey: ["getResources"],
        queryFn: () => getResources(getAccessTokenSilently, worldId),
    });
};
