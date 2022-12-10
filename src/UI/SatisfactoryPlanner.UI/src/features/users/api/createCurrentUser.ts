import * as Config from "../../../config";

export const createCurrentUser = async (
    getAccessTokenSilently: any,
    auth0UserId: string
): Promise<string> => {
    const baseUrl = Config.API_URL;
    const accessToken = await getAccessTokenSilently({
        audience: baseUrl,
    });

    const response = await fetch(baseUrl + "/user-access/users/@me", {
        method: "POST",
        body: JSON.stringify({
            auth0UserId: auth0UserId,
        }),
        headers: {
            // Add the Authorization header to the existing headers
            Accept: "application/json",
            Authorization: `Bearer ${accessToken}`,
            "Content-Type": "application/json",
        },
    });

    if (!response.ok) throw new Error(response.statusText);
    if (response.status === 204) return "";

    return response.json();
};
