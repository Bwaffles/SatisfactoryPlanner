import { PromiseFn } from "react-async";
import { useAsync } from "react-async";
import { useAuth0 } from "@auth0/auth0-react";

import { CurrentUser } from "../types";
import * as Config from "../../../config";

import makeDebugger from "../../../utils/makeDebugger";
const debug = makeDebugger("getCurrentUser");

export const getCurrentUser: PromiseFn<any> = async (
    { getAccessTokenSilently },
    { signal }: { signal: AbortSignal | null }
) => {
    const baseUrl = Config.API_URL;
    const accessToken = await getAccessTokenSilently({
        audience: baseUrl,
    });

    const response = await fetch(baseUrl + "/user-access/users/@me", {
        method: "GET",
        signal,
        headers: {
            // Add the Authorization header to the existing headers
            Accept: "application/json",
            Authorization: `Bearer ${accessToken}`,
            "Content-Type": "application/json",
        },
    });

    debug(response);

    if (!response.ok) throw new Error(response.statusText);
    if (response.status === 204) return "";

    return response.json();
};

export const useCurrentUser = () => {
    const { getAccessTokenSilently } = useAuth0();

    return useAsync<CurrentUser>({
        promiseFn: getCurrentUser,
        suspense: true,
        getAccessTokenSilently,
    });
};
