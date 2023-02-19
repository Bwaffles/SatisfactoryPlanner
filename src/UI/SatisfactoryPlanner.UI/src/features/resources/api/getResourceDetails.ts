import { useAuth0 } from "@auth0/auth0-react";
import { useQuery } from "react-query";

import * as Config from "../../../config";
import { ResourceDetails } from "../types";

export const getResourceDetails = async (
    getAccessTokenSilently: any,
    resource: string
): Promise<ResourceDetails> => {
    const baseUrl = Config.API_URL;
    const accessToken = await getAccessTokenSilently({
        audience: baseUrl,
    });

    const response = await fetch(baseUrl + `/resources/resources/${resource}`, {
        method: "GET",
        headers: {
            // Add the Authorization header to the existing headers
            Accept: "application/json",
            Authorization: `Bearer ${accessToken}`,
            "Content-Type": "application/json",
        },
    });

    if (!response.ok) throw new Error(response.statusText);
    return response.json();
};

export const useGetResourceDetails = (resourceId: string) => {
    const { getAccessTokenSilently } = useAuth0();
    return useQuery("getResourceDetails", () =>
        getResourceDetails(getAccessTokenSilently, resourceId)
    );
};
