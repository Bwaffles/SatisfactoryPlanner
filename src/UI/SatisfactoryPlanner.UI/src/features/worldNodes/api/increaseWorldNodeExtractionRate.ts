import { useAuth0 } from "@auth0/auth0-react";
import { useMutation } from "react-query";

import * as Config from "../../../config";
import { queryClient } from "../../../lib/react-query";
import storage from "../../../utils/storage";

const increaseWorldNodeExtractionRate = async (
    getAccessTokenSilently: any,
    worldId: string,
    nodeId: string,
    extractionRate: number
): Promise<string> => {
    const baseUrl = Config.API_URL;
    const accessToken = await getAccessTokenSilently({
        audience: baseUrl,
    });

    const response = await fetch(
        baseUrl + `/worlds/${worldId}/nodes/${nodeId}/increase-extraction-rate`,
        {
            method: "POST",
            body: JSON.stringify({
                worldId,
                extractionRate,
            }),
            headers: {
                // Add the Authorization header to the existing headers
                Accept: "application/json",
                Authorization: `Bearer ${accessToken}`,
                "Content-Type": "application/json",
            },
        }
    );

    if (!response.ok) throw new Error(response.statusText);
    return "";
};

type IncreaseWorldNodeExtractionRateRequest = {
    nodeId: string;
    extractionRate: number;
};

export const useIncreaseWorldNodeExtractionRate = () => {
    const { getAccessTokenSilently } = useAuth0();
    const worldId = storage.getWorldId();

    return useMutation({
        onSuccess: () => {
            // Invalidating queries that show whether a node has been tapped or not

            // Let these ones update behind the scenes since they're not as likely to be needed so fast
            queryClient.invalidateQueries("getResources"); // resource extraction rate totals
            queryClient.invalidateQueries("getWorldNodes"); // node extraction rate

            // Wait until getWorldNodeDetails finishes updating before ending the mutation so that the node details page updates
            return queryClient.invalidateQueries({
                queryKey: ["getWorldNodeDetails"],
            });
        },
        mutationFn: (variables: IncreaseWorldNodeExtractionRateRequest) => {
            return increaseWorldNodeExtractionRate(
                getAccessTokenSilently,
                worldId,
                variables.nodeId,
                variables.extractionRate
            );
        },
    });
};
