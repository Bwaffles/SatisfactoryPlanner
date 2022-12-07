import { PromiseFn } from "react-async";
import { useAsync } from "react-async";

import { useApi } from "../../../hooks/use-api";
import { CurrentPioneerWorld } from "../types";

export const getCurrentPioneerWorlds: PromiseFn<any> = async (
    { api },
    { signal }: { signal: AbortSignal | null }
) => {
    const response = await api("/worlds/worlds/@me", {
        method: "GET",
        signal,
    });
    if (!response.response?.ok) throw new Error(response.response?.statusText);
    return response.response?.json();
};

export const useCurrentPioneerWorlds = () => {
    const api = useApi();
    return useAsync<CurrentPioneerWorld[]>({
        promiseFn: getCurrentPioneerWorlds,
        suspense: true,
        api,
    });
};
