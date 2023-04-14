import React from "react";
import { useParams } from "react-router-dom";
import { ContentLayout } from "../components/Layout/ContentLayout";

import { useGetNodeDetails } from "../features/nodes/api/getNodeDetails";

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
            {nodeDetails!.isTapped && (
                <div className="flex flex-col items-center gap-4 p-14 bg-gray-800 rounded">
                    <span className="text-2xl font-bold">Node Tapped</span>
                </div>
            )}
            {!nodeDetails!.isTapped && (
                <div className="flex flex-col items-center gap-4 p-14 bg-gray-800 rounded">
                    <span className="text-2xl font-bold">Tap Node</span>
                </div>
            )}
        </ContentLayout>
    );
};
