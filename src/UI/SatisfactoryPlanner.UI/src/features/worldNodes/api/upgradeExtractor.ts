import { useAuth0 } from "@auth0/auth0-react";
import { useMutation } from "react-query";

import * as Config from "../../../config";
import { queryClient } from "../../../lib/react-query";
import { axios } from "../../../lib/axios";
import storage from "../../../utils/storage";

const upgradeExtractor = async (
    getAccessTokenSilently: any,
    worldId: string,
    nodeId: string,
    extractorId: string
): Promise<string> => {
    const baseUrl = Config.API_URL;
    const accessToken = await getAccessTokenSilently({
        audience: baseUrl,
    });

    return axios.post(
        `/worlds/${worldId}/nodes/${nodeId}/upgrade-extractor`,
        {
            extractorId,
        },
        {
            headers: {
                Authorization: `Bearer ${accessToken}`,
            },
        }
    );
};

type UpgradeExtractorRequest = {
    nodeId: string;
    extractorId: string;
};

export const useUpgradeExtractor = () => {
    const { getAccessTokenSilently } = useAuth0();
    const worldId = storage.getWorldId();

    return useMutation({
        onSuccess: () => {
            // Invalidating queries that show current extractor

            // Wait until getWorldNodeDetails finishes updating before ending the mutation so that the world node details page updates
            return queryClient.invalidateQueries({
                queryKey: ["getWorldNodeDetails"],
            });
        },
        mutationFn: (variables: UpgradeExtractorRequest) => {
            return upgradeExtractor(
                getAccessTokenSilently,
                worldId,
                variables.nodeId,
                variables.extractorId
            );
        },
    });
};
