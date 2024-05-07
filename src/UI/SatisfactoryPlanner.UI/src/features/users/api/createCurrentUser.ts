import { ApiClient } from "lib/api";

export const createCurrentUser = async (
  api: ApiClient,
  auth0UserId: string
): Promise<string> => {
  return await api.post(`/user-access/users/@me`, {
    auth0UserId: auth0UserId,
  });
};
