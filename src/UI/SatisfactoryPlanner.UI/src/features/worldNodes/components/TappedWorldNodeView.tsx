import * as React from "react";
import { NavigateFunction, useNavigate } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus, faMinus } from "@fortawesome/free-solid-svg-icons";

import { Button } from "../../../components/Elements/Button";
import { formatNumber } from "../../../utils/format";
import { AvailableExtractor, WorldNodeDetails } from "../types";
import { FieldWrapper } from "../../../components/Elements/Form/FieldWrapper";

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
            <div className="flex flex-col gap-6 p-6 w-1/2 bg-gray-800 rounded">
                <FieldWrapper label="Purity">
                    <div className={"text-xl font-bold " + purityTextColor}>
                        {worldNodeDetails.purity}
                    </div>
                </FieldWrapper>

                <FieldWrapper label="Extractor">
                    <div className="text-xl font-bold">
                        {currentExtractor.name}
                    </div>
                </FieldWrapper>

                <FieldWrapper label="Max Extraction Rate">
                    <div className="flex">
                        <div className="text-xl font-bold">
                            {formatNumber(currentExtractor.maxExtractionRate)}
                        </div>
                        <div className="ml-2 text-gray-400 text-xs leading-8">
                            per min
                        </div>
                    </div>
                </FieldWrapper>

                <FieldWrapper label="Extraction Rate">
                    {renderExtractionRate(
                        navigate,
                        worldNodeDetails,
                        currentExtractor
                    )}
                </FieldWrapper>
            </div>
        </>
    );
};

function renderExtractionRate(
    navigate: NavigateFunction,
    worldNodeDetails: WorldNodeDetails,
    currentExtractor: AvailableExtractor
) {
    const canDecreaseRate = worldNodeDetails.extractionRate > 0;
    const canIncreaseRate =
        worldNodeDetails.extractionRate < currentExtractor.maxExtractionRate;

    return (
        <div className="relative flex mb-4">
            {canDecreaseRate && (
                <Button
                    className="py-2 px-2 rounded-r-none rounded-l"
                    title="Decrease the extraction rate"
                    onClick={() => navigate(`decrease-extraction-rate`)}
                >
                    <FontAwesomeIcon icon={faMinus} />
                </Button>
            )}
            <input
                type="text"
                className={
                    "p-2 pr-0 w-24 text-right border border-solid bg-gray-700 border-gray-600 text-gray-200 " +
                    (canDecreaseRate ? "border-x-0" : "border-r-0 rounded-l")
                }
                value={formatNumber(worldNodeDetails.extractionRate)}
                disabled={true}
            />
            <span
                className={
                    "p-2 text-xs leading-6 border border-solid bg-gray-700 border-gray-600 text-gray-400 " +
                    (canIncreaseRate ? "border-x-0" : "border-l-0 rounded-r")
                }
            >
                per min
            </span>
            {canIncreaseRate && (
                <Button
                    className="py-2 px-2 rounded-l-none rounded-r"
                    title="Incease the extraction rate"
                    onClick={() => navigate(`increase-extraction-rate`)}
                >
                    <FontAwesomeIcon icon={faPlus} />
                </Button>
            )}
        </div>
    );
}
