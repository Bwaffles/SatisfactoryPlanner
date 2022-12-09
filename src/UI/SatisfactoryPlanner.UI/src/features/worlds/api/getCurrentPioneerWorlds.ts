import { useAuth0 } from "@auth0/auth0-react";
import { useQuery } from "react-query";

import * as Config from "../../../config";
import { CurrentPioneerWorld } from "../types";

export const getCurrentPioneerWorlds = async (
    getAccessTokenSilently: any
): Promise<CurrentPioneerWorld[]> => {
    const baseUrl = Config.API_URL;
    const accessToken = await getAccessTokenSilently({
        audience: baseUrl,
    });

    const response = await fetch(baseUrl + "/worlds/worlds/@me", {
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

export const useCurrentPioneerWorlds = () => {
    const { getAccessTokenSilently } = useAuth0();
    return useQuery("getCurrentPioneersWorlds", () =>
        getCurrentPioneerWorlds(getAccessTokenSilently)
    );
};
