import { useAuth0 } from "@auth0/auth0-react";
import { useQuery } from "react-query";

import * as Config from "../../../config";
import { ResourceDetails } from "../types";
import { axios } from "../../../lib/axios";

export const getResourceDetails = async (
    getAccessTokenSilently: any,
    resource: string
): Promise<ResourceDetails> => {
    const accessToken = await getAccessTokenSilently({
        audience: Config.API_URL,
    });

    return axios.get(`/resources/resources/${resource}`, {
        headers: {
            Authorization: `Bearer ${accessToken}`,
        },
    });
};

export const useGetResourceDetails = (resourceId: string) => {
    const { getAccessTokenSilently } = useAuth0();
    return useQuery({
        queryKey: ["getResourceDetails"],
        queryFn: () => getResourceDetails(getAccessTokenSilently, resourceId),
    });
};
