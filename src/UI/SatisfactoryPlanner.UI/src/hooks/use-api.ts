import { useAuth0 } from "@auth0/auth0-react";

import makeDebugger from "../utils/makeDebugger";
const debug = makeDebugger("useApi");

export interface ApiResponse {
    error: unknown;
    statusCode: number;
    data: any;
    response: Response | null;
}

export const useApi = () => {
    const { getAccessTokenSilently } = useAuth0();

    const fetchResponse = async (url: string, options: any = {}) => {
        try {
            const baseUrl = "http://localhost:55915/api";
            const { method, ...fetchOptions } = options;
            const accessToken = await getAccessTokenSilently({
                audience: baseUrl,
            });

            debug(`Fetching ${method} ${url}...`);

            const res = await fetch(baseUrl + url, {
                method,
                ...fetchOptions,
                headers: {
                    ...fetchOptions.headers,
                    // Add the Authorization header to the existing headers
                    Authorization: `Bearer ${accessToken}`,
                    "Content-Type": "application/json",
                },
            });

            debug("Response", res);

            // I have to do this because when my body is null, calling json fails and I have calls
            // that return null when they're successful.
            var data: any;
            try {
                data = await res.clone().json(); // Calling clone because you can't read the body twice
            } catch (e) {
                debug("Can't read body as json: ", e);
            }

            if (data === undefined) {
                data = await res.text();
            }

            var response: ApiResponse = {
                error: null,
                statusCode: res.status,
                data: data,
                response: res,
            };
            return response;
        } catch (error) {
            var errorResponse: ApiResponse = {
                error: error,
                statusCode: 0,
                data: null,
                response: null,
            };
            return errorResponse;
        }
    };

    return fetchResponse;
};
