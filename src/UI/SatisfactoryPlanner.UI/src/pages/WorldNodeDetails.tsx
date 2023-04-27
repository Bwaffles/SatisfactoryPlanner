import React from "react";
import { useParams } from "react-router-dom";

import { ContentLayout } from "../components/Layout/ContentLayout";
import { useGetWorldNodeDetails } from "../features/worldNodes/api/getWorldNodeDetails";
import { TappedWorldNodeView } from "../features/worldNodes/components/TappedWorldNodeView";
import { UntappedWorldNodeView } from "../features/worldNodes/components/UntappedWorldNodeView";

export const WorldNodeDetails = () => {
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
                <TappedWorldNodeView worldNodeDetails={worldNodeDetails!} />
            ) : (
                <UntappedWorldNodeView worldNodeDetails={worldNodeDetails!} />
            )}
        </ContentLayout>
    );
};
