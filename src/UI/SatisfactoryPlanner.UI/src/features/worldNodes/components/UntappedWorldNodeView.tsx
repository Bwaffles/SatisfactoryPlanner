import React, { useState } from "react";

import Doggo from "../../../assets/Lizard_Doggo.png";
import { Button } from "../../../components/Elements/Button";
import { formatNumber } from "../../../utils/format";
import { useTapWorldNode } from "../api/tapWorldNode";
import { WorldNodeDetails } from "../types";
import { FieldWrapper } from "../../../components/Elements/Form/FieldWrapper";

type UntappedWorldNodeViewProps = {
    worldNodeDetails: WorldNodeDetails;
};

export const UntappedWorldNodeView = ({
    worldNodeDetails,
}: UntappedWorldNodeViewProps) => {
    const [selectedExtractor, setSelectedExtractor] = useState<string | null>(
        null
    );
    const [validationMessage, setValidationMessage] = useState<string | null>(
        null
    );
    const tapNodeMutation = useTapWorldNode();

    return (
        <>
            <div className="flex flex-col gap-6 p-6 w-fit bg-gray-800 rounded">
                <p>Select the extractor to tap the node with:</p>
                <div className="flex flex-wrap gap-12">
                    {worldNodeDetails.availableExtractors?.map((extractor) => {
                        var selectedExtractorClasses =
                            extractor.id === selectedExtractor
                                ? "bg-sky-800 border-white-900"
                                : "bg-gray-700 hover:bg-sky-900 cursor-pointer border-transparent";

                        return (
                            <div
                                key={extractor.id}
                                className={
                                    "flex flex-col items-center rounded p-6 w-64 border-4 " +
                                    selectedExtractorClasses
                                }
                                onClick={
                                    tapNodeMutation.isLoading
                                        ? () => {}
                                        : () => {
                                              setSelectedExtractor(
                                                  extractor.id
                                              );
                                              setValidationMessage(null);
                                          }
                                }
                            >
                                <div className="text-lg font-bold">
                                    {extractor.name}
                                </div>
                                <img
                                    className="h-40 w-40 text-center"
                                    alt="Extractor"
                                    src={Doggo}
                                ></img>
                                <FieldWrapper
                                    label="Max Extraction Rate"
                                    className="col-auto"
                                >
                                    <div className="flex">
                                        <div className={"text-xl font-bold"}>
                                            {formatNumber(
                                                extractor.maxExtractionRate
                                            )}
                                        </div>
                                        <div className="ml-2 text-gray-400 text-xs leading-8">
                                            per min
                                        </div>
                                    </div>
                                </FieldWrapper>
                            </div>
                        );
                    })}
                </div>
                {validationMessage != null && (
                    <div className="text-red-500">{validationMessage}</div>
                )}
                <div>
                    <Button
                        isLoading={tapNodeMutation.isLoading}
                        className="mt-4"
                        onClick={() => {
                            if (selectedExtractor == null) {
                                setValidationMessage("Select an extractor.");
                                return;
                            }

                            tapNodeMutation.mutate({
                                nodeId: worldNodeDetails.nodeId,
                                extractorId: selectedExtractor!,
                            });
                        }}
                    >
                        Tap
                    </Button>
                </div>
            </div>
        </>
    );
};
