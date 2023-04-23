import { useAuth0 } from "@auth0/auth0-react";
import { useQuery } from "react-query";

import * as Config from "../../../config";
import storage from "../../../utils/storage";
import { WorldNodeDetails } from "../types";

export const getWorldNodeDetails = async (
    getAccessTokenSilently: any,
    worldId: string,
    nodeId: string
): Promise<WorldNodeDetails> => {
    const baseUrl = Config.API_URL;
    const accessToken = await getAccessTokenSilently({
        audience: baseUrl,
    });

    const response = await fetch(
        baseUrl + `/worlds/${worldId}/nodes/${nodeId}`,
        {
            method: "GET",
            headers: {
                // Add the Authorization header to the existing headers
                Accept: "application/json",
                Authorization: `Bearer ${accessToken}`,
                "Content-Type": "application/json",
            },
        }
    );

    if (!response.ok) throw new Error(response.statusText);
    return response.json();
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
