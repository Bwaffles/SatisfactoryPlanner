import { useAuth0 } from "@auth0/auth0-react";
import { useAsync, DeferFn } from "react-async";

import * as Config from "../../../config";

import makeDebugger from "../../../utils/makeDebugger";
const debug = makeDebugger("getCurrentUser");

const createCurrentUser: DeferFn<any> = async (
    [],
    props,
    { signal }: { signal: AbortSignal | null }
) => {
    const baseUrl = Config.API_URL;
    const accessToken = await props.getAccessTokenSilently({
        audience: baseUrl,
    });

    const response = await fetch(baseUrl + "/user-access/users/@me", {
        method: "POST",
        body: JSON.stringify({
            auth0UserId: props.auth0UserId,
        }),
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

export const useCreateCurrentUser = () => {
    const { getAccessTokenSilently, user } = useAuth0();
    const auth0UserId = user!.sub;

    return useAsync({
        deferFn: createCurrentUser,
        suspense: true,
        auth0UserId,
        getAccessTokenSilently,
    });
};
