import * as Config from "config";
import { CurrentUser } from "../types";

export const getCurrentUser = async (
  getAccessTokenSilently: any
): Promise<CurrentUser | undefined> => {
  const baseUrl = Config.API_URL;
  const accessToken = await getAccessTokenSilently({
    audience: baseUrl,
  });

  const response = await fetch(baseUrl + "/user-access/users/@me", {
    method: "GET",
    headers: {
      // Add the Authorization header to the existing headers
      Accept: "application/json",
      Authorization: `Bearer ${accessToken}`,
      "Content-Type": "application/json",
    },
  });

  if (!response.ok) throw new Error(response.statusText);
  if (response.status === 204) return undefined;

  return response.json();
};
