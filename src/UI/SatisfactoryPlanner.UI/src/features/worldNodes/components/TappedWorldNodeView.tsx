import * as React from "react";
import { useNavigate } from "react-router-dom";

import { Button } from "../../../components/Elements/Button";
import { formatNumber } from "../../../utils/format";
import { WorldNodeDetails } from "../types";

type TappedWorldNodeViewProps = {
    worldNodeDetails: WorldNodeDetails;
};

export const TappedWorldNodeView = ({
    worldNodeDetails,
}: TappedWorldNodeViewProps) => {
    const navigate = useNavigate();
    var purityTextColor =
        worldNodeDetails.purity === "Pure"
            ? "text-green-600"
            : worldNodeDetails.purity === "Normal"
            ? "text-yellow-600"
            : worldNodeDetails.purity === "Impure"
            ? "text-red-600"
            : "";

    const currentExtractor = worldNodeDetails.availableExtractors.find(
        (extractor) => extractor.id === worldNodeDetails.extractorId
    )!;

    return (
        <>
            <div className="flex flex-col p-6 bg-gray-800 rounded">
                <label className="inline-block text-gray-400 mb-2">
                    Purity
                </label>
                <div className={"text-xl font-bold mb-6 " + purityTextColor}>
                    {worldNodeDetails.purity}
                </div>

                <label className="inline-block text-gray-400 mb-2">
                    Extractor
                </label>
                <div className="flex mb-6">
                    <div className="text-xl font-bold">
                        {currentExtractor.name}
                    </div>
                </div>

                <label className="inline-block text-gray-400 mb-2">
                    Max Extraction Rate
                </label>
                <div className="flex mb-6">
                    <div className="text-xl font-bold">
                        {formatNumber(currentExtractor.maxExtractionRate)}
                    </div>
                    <div className="ml-2 text-gray-400 text-xs leading-8">
                        per min
                    </div>
                </div>

                <label className="inline-block text-gray-400 mb-2">
                    Extraction Rate
                </label>
                <div className="relative flex mb-4">
                    <input
                        type="text"
                        className="p-2 pr-0 w-24 text-right rounded-l border border-r-0 border-solid bg-gray-800 border-gray-600 text-gray-200"
                        value={formatNumber(worldNodeDetails.extractionRate)}
                        disabled={true}
                    />
                    <span className="p-2 text-xs leading-6 border border-l-0 border-r-0 border-solid bg-gray-800 border-gray-600 text-gray-400">
                        per min
                    </span>
                    <Button
                        className="py-2 px-3 rounded-l-none rounded-r"
                        onClick={() => navigate(`increase-extraction-rate`)}
                    >
                        Increase
                    </Button>
                </div>
            </div>
        </>
    );
};