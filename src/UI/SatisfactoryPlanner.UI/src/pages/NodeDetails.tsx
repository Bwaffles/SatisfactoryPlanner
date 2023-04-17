import React, { useState } from "react";
import { useParams } from "react-router-dom";
import { ContentLayout } from "../components/Layout/ContentLayout";

import { useGetNodeDetails } from "../features/nodes/api/getNodeDetails";

export const NodeDetails = () => {
    const { nodeId } = useParams();
    const { isError, data: nodeDetails, error } = useGetNodeDetails(nodeId!);
    const [isTapping, setIsTapping] = useState(false);

    const handleTapNode = () => {
        alert("tapping node");
        setIsTapping(false);
    };

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
            {!nodeDetails!.isTapped && !isTapping && (
                <div
                    className="flex flex-col items-center gap-4 p-14 bg-gray-800 rounded hover:bg-sky-900 cursor-pointer"
                    onClick={() => {
                        setIsTapping(true);
                    }}
                >
                    <span className="text-2xl font-bold">Tap Node</span>
                </div>
            )}
            {isTapping && (
                <div className="flex flex-col gap-4 p-6 bg-gray-800 rounded">
                    <p className="text-lg font-bold mb-4">
                        Select the extractor to tap the node with
                    </p>
                    [Insert list of extractors here]
                    <div>
                        <button
                            className="w-1/2 py-4 px-4 text-white rounded-l bg-sky-800 hover:bg-sky-700"
                            onClick={() => handleTapNode()}
                        >
                            Tap
                        </button>
                        <button
                            className="w-1/2 py-4 px-4 text-white rounded-r bg-red-800 hover:bg-red-700"
                            onClick={() => setIsTapping(false)}
                        >
                            Cancel
                        </button>
                    </div>
                </div>
            )}
        </ContentLayout>
    );
};
