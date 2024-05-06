import { CurrentUser } from "../types";
import { ApiClient } from "lib/api";

export const getCurrentUser = async (
  api: ApiClient
): Promise<CurrentUser | undefined> => {
  var response = await api.get(`/user-access/users/@me`);
  if (response == "") return undefined;
  else {
    return response;
  }
};
