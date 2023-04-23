import { useAuth0 } from "@auth0/auth0-react";
import { useQuery } from "react-query";

import * as Config from "../../../config";
import storage from "../../../utils/storage";
import { WorldNode } from "../types";

export const getWorldNodes = async (
    getAccessTokenSilently: any,
    worldId: any,
    resourceId: string
): Promise<WorldNode[]> => {
    const baseUrl = Config.API_URL;
    const accessToken = await getAccessTokenSilently({
        audience: baseUrl,
    });

    const response = await fetch(
        baseUrl + `/worlds/${worldId}/nodes?resourceId=${resourceId}`,
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

export const useGetWorldNodes = (resourceId: string) => {
    const { getAccessTokenSilently } = useAuth0();
    const worldId = storage.getWorldId();
    return useQuery("getWorldNodes", () =>
        getWorldNodes(getAccessTokenSilently, worldId, resourceId)
    );
};
