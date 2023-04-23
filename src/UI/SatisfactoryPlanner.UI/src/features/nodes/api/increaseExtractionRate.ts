import { useAuth0 } from "@auth0/auth0-react";
import { useMutation } from "react-query";

import * as Config from "../../../config";
import { queryClient } from "../../../lib/react-query";
import storage from "../../../utils/storage";

const increaseExtractionRate = async (
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
        baseUrl + `/resources/nodes/${nodeId}/increase-extraction-rate`,
        {  // TODO move increase extraction rate to NodeController? I don't like ui having to know about tapped node objects
            // => I think it does make sense
            // TODO how about when the user clicks Increase button I take them to a new page? Not sure if I want to use modals
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
    return response.json();
};

type IncreaseExtractionRateRequest = {
    nodeId: string;
    extractionRate: number;
};

export const useIncreaseExtractionRate = () => {
    const { getAccessTokenSilently } = useAuth0();
    const worldId = storage.getWorldId();

    return useMutation({
        onSuccess: (_, variables) => {
            // Invalidating queries that show whether a node has been tapped or not

            // Let these ones update behind the scenes since they're not as likely to be needed so fast
            queryClient.invalidateQueries("getResources"); // resource extraction rate totals
            queryClient.invalidateQueries("getNodes"); // node extraction rate

            // Wait until getNodeDetails finishes updating before ending the mutation so that the node details page updates
            return queryClient.invalidateQueries({
                queryKey: ["getNodeDetails", variables.nodeId, worldId],
            });
        },
        mutationFn: (variables: IncreaseExtractionRateRequest) => {
            return increaseExtractionRate(
                getAccessTokenSilently,
                worldId,
                variables.nodeId,
                variables.extractionRate
            );
        },
    });
};
