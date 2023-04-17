import React from "react";
import { useParams } from "react-router-dom";
import { ContentLayout } from "../components/Layout/ContentLayout";

import { useGetNodeDetails } from "../features/nodes/api/getNodeDetails";
import { TappedNodeView } from "../features/nodes/components/TappedNodeView";
import { UntappedNodeView } from "../features/nodes/components/UntappedNodeView";

export const NodeDetails = () => {
    const { nodeId } = useParams();
    const { isError, data: nodeDetails, error } = useGetNodeDetails(nodeId!);

    if (isError) {
        return <span>Error: {(error as Error).message}</span>;
    }

    var nodeName = `${nodeDetails!.resourceName} - ${nodeDetails!.biome} ${
        nodeDetails!.number
    }`;

    return (
        <ContentLayout title={nodeName}>
            {nodeDetails!.isTapped ? (
                <TappedNodeView />
            ) : (
                <UntappedNodeView nodeDetails={nodeDetails!} />
            )}
        </ContentLayout>
    );
};
