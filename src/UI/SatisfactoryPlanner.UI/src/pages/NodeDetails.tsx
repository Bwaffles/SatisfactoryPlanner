import React from "react";
import { useParams } from "react-router-dom";
import { ContentLayout } from "../components/Layout/ContentLayout";

import { useGetWorldNodeDetails } from "../features/nodes/api/getWorldNodeDetails";
import { TappedNodeView } from "../features/nodes/components/TappedNodeView";
import { UntappedNodeView } from "../features/nodes/components/UntappedNodeView";

export const NodeDetails = () => {
    const { nodeId } = useParams();
    const {
        isError,
        data: worldNodeDetails,
        error,
    } = useGetWorldNodeDetails(nodeId!);

    if (isError) {
        return <span>Error: {(error as Error).message}</span>;
    }

    var nodeName = `${worldNodeDetails!.resourceName} - ${
        worldNodeDetails!.biome
    } ${worldNodeDetails!.number}`;

    return (
        <ContentLayout title={nodeName}>
            {worldNodeDetails!.isTapped ? (
                <TappedNodeView worldNodeDetails={worldNodeDetails!} />
            ) : (
                <UntappedNodeView worldNodeDetails={worldNodeDetails!} />
            )}
        </ContentLayout>
    );
};
