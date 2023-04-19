import { useAuth0 } from "@auth0/auth0-react";
import { useMutation } from "react-query";

import * as Config from "../../../config";
import { queryClient } from "../../../lib/react-query";
import storage from "../../../utils/storage";

const tapNode = async (
    getAccessTokenSilently: any,
    worldId: string,
    nodeId: string,
    extractorId: string
): Promise<string> => {
    const baseUrl = Config.API_URL;
    const accessToken = await getAccessTokenSilently({
        audience: baseUrl,
    });

    const response = await fetch(baseUrl + `/resources/nodes/${nodeId}/tap`, {
        method: "POST",
        body: JSON.stringify({
            worldId: worldId,
            extractorId: extractorId,
        }),
        headers: {
            // Add the Authorization header to the existing headers
            Accept: "application/json",
            Authorization: `Bearer ${accessToken}`,
            "Content-Type": "application/json",
        },
    });

    if (!response.ok) throw new Error(response.statusText);
    return response.json();
};

type TapNodeRequest = {
    nodeId: string;
    extractorId: string;
};

export const useTapNode = () => {
    const { getAccessTokenSilently } = useAuth0();
    const worldId = storage.getWorldId();

    return useMutation({
        onSuccess: (_, variables) => {
            // Invalidating queries that show whether a node has been tapped or not

            // Let this one update behind the scenes since it's not as likely to be needed so fast
            queryClient.invalidateQueries("getNodes");

            // Wait until getNodeDetails finishes updating before ending the mutation so that the node details page updates
            return queryClient.invalidateQueries({
                queryKey: ["getNodeDetails", variables.nodeId, worldId],
            });
        },
        mutationFn: (variables: TapNodeRequest) => {
            return tapNode(
                getAccessTokenSilently,
                worldId,
                variables.nodeId,
                variables.extractorId
            );
        },
    });
};
