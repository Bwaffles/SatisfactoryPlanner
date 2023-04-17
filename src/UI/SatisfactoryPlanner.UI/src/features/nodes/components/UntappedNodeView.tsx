import React from "react";

import Doggo from "../../../assets/Lizard_Doggo.png";
import { formatNumber } from "../../../utils/format";
import { NodeDetails } from "../types";

type UntappedNodeViewProps = {
    nodeDetails: NodeDetails;
};

export const UntappedNodeView = ({
    nodeDetails: nodeDetails,
}: UntappedNodeViewProps) => {
    const handleTapNode = () => {
        alert("tapping node");
    };

    return (
        <>
            <div className="flex flex-col gap-4 p-6 bg-gray-800 rounded">
                <p className="mb-4">
                    Select the extractor to tap the node with
                </p>
                <div className="flex gap-12 mb-4">
                    {nodeDetails.availableExtractors?.map((extractor) => {
                        return (
                            <div
                                key={extractor.id}
                                className="flex flex-col items-center rounded bg-gray-700 p-6"
                            >
                                <div className="text-lg font-bold">
                                    {extractor.name}
                                </div>
                                <img
                                    className="h-40 w-40 text-center"
                                    alt="Icon Image"
                                    src={Doggo}
                                ></img>
                                <div>
                                    <div className="text-xs text-gray-400">
                                        Max Extractable
                                    </div>
                                    <div className="text-lg text-right">
                                        {formatNumber(
                                            extractor.maxAmountExtractable
                                        )}
                                    </div>
                                </div>
                            </div>
                        );
                    })}
                </div>
                <div>
                    <button
                        className="w-full py-4 px-4 text-white rounded bg-sky-800 hover:bg-sky-700"
                        onClick={() => handleTapNode()}
                    >
                        Tap
                    </button>
                </div>
            </div>
        </>
    );
};
