import { useAuth0 } from "@auth0/auth0-react";
import { useQuery } from "react-query";

import * as Config from "../../../config";
import storage from "../../../utils/storage";
import { NodeDetails } from "../types";

export const getNodeDetails = async (
    getAccessTokenSilently: any,
    nodeId: string
): Promise<NodeDetails> => {
    const baseUrl = Config.API_URL;
    const accessToken = await getAccessTokenSilently({
        audience: baseUrl,
    });

    const worldId = storage.getWorldId();
    const response = await fetch(
        baseUrl + `/resources/nodes/${nodeId}?worldId=${worldId}`,
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

export const useGetNodeDetails = (nodeId: string) => {
    const { getAccessTokenSilently } = useAuth0();
    return useQuery("getNodeDetails", () =>
        getNodeDetails(getAccessTokenSilently, nodeId)
    );
};
