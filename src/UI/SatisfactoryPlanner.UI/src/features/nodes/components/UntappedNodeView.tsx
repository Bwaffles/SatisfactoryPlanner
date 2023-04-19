import React, { useState } from "react";

import Doggo from "../../../assets/Lizard_Doggo.png";
import { formatNumber } from "../../../utils/format";
import { useTapNode } from "../api/tapNode";
import { NodeDetails } from "../types";

type UntappedNodeViewProps = {
    nodeDetails: NodeDetails;
};

export const UntappedNodeView = ({
    nodeDetails: nodeDetails,
}: UntappedNodeViewProps) => {
    const [selectedExtractor, setSelectedExtractor] = useState<string | null>(
        null
    );
    const tapNodeMutation = useTapNode();

    return (
        <>
            <div className="flex flex-col gap-4 p-6 bg-gray-800 rounded">
                <p className="mb-4">
                    Select the extractor to tap the node with:
                </p>
                <div className="flex gap-12 mb-4">
                    {nodeDetails.availableExtractors?.map((extractor) => {
                        var selectedExtractorClasses =
                            extractor.id == selectedExtractor
                                ? "bg-sky-800 border-white-900"
                                : "bg-gray-700 hover:bg-sky-900 cursor-pointer border-transparent";

                        return (
                            <div
                                key={extractor.id}
                                className={
                                    "flex flex-col items-center rounded p-6 border-4 " +
                                    selectedExtractorClasses
                                }
                                onClick={() =>
                                    setSelectedExtractor(extractor.id)
                                }
                            >
                                <div className="text-lg font-bold">
                                    {extractor.name}
                                </div>
                                <img
                                    className="h-40 w-40 text-center"
                                    alt="Extractor Image"
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
                        onClick={() =>
                            tapNodeMutation.mutate({
                                nodeId: nodeDetails.id,
                                extractorId: selectedExtractor!,
                            })
                        }
                    >
                        Tap
                    </button>
                </div>
            </div>
        </>
    );
};
