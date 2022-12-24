import { useAuth0 } from "@auth0/auth0-react";
import { useQuery } from "react-query";

import * as Config from "../../../config";
import { Resource } from "../types";

export const getResources = async (
    getAccessTokenSilently: any
): Promise<Resource[]> => {
    const baseUrl = Config.API_URL;
    const accessToken = await getAccessTokenSilently({
        audience: baseUrl,
    });

    const response = await fetch(baseUrl + "/resources/resources", {
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

export const useGetResources = () => {
    const { getAccessTokenSilently } = useAuth0();
    return useQuery("getResources", () => getResources(getAccessTokenSilently));
};
