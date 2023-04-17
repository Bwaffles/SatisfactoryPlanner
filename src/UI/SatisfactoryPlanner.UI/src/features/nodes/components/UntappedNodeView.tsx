import React, { useState } from "react";

import Doggo from "../../../assets/Lizard_Doggo.png";
import { formatNumber } from "../../../utils/format";
import { NodeDetails } from "../types";

type UntappedNodeViewProps = {
    nodeDetails: NodeDetails;
};

export const UntappedNodeView = ({
    nodeDetails: nodeDetails,
}: UntappedNodeViewProps) => {
    const [isTapping, setIsTapping] = useState(false);

    const handleTapNode = () => {
        alert("tapping node");
        setIsTapping(false);
    };

    return (
        <>
            {!isTapping ? (
                <div
                    className="flex flex-col items-center gap-4 p-14 bg-gray-800 rounded hover:bg-sky-900 cursor-pointer"
                    onClick={() => {
                        setIsTapping(true);
                    }}
                >
                    <span className="text-2xl font-bold">Tap Node</span>
                </div>
            ) : (
                <div className="flex flex-col gap-4 p-6 bg-gray-800 rounded">
                    <p className="text-lg font-bold mb-4">
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
        </>
    );
};
