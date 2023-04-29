import React, { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import { ContentLayout } from "../components/Layout/ContentLayout";
import { useGetWorldNodeDetails } from "../features/worldNodes/api/getWorldNodeDetails";
import { formatNumber } from "../utils/format";
import { Button } from "../components/Elements/Button";
import { useIncreaseWorldNodeExtractionRate } from "../features/worldNodes/api/increaseWorldNodeExtractionRate";
import { ErrorResponse } from "../lib/axios";
import { FieldWrapper } from "../components/Elements/Form/FieldWrapper";

export const IncreaseWorldNodeExtractionRate = () => {
    const { nodeId } = useParams();
    const [extractionRate, setExtractionRate] = useState<number | null>(null);
    const [errorMessages, setErrorMessages] = useState<string[] | null>(null);
    const navigate = useNavigate();
    const {
        isError,
        data: worldNodeDetails,
        error,
    } = useGetWorldNodeDetails(nodeId!);

    const increaseWorldNodeExtractionRateMutation =
        useIncreaseWorldNodeExtractionRate();

    if (isError) {
        return <span>Error: {(error as Error).message}</span>;
    }

    const currentExtractor = worldNodeDetails!.availableExtractors.find(
        (extractor) => extractor.id === worldNodeDetails!.extractorId
    )!;

    return (
        <ContentLayout title="Increase Extraction Rate">
            <div className="p-6 bg-gray-800 rounded w-1/2 grid grid-cols-2 gap-y-6">
                <FieldWrapper
                    label="Current Extraction Rate"
                    className="col-auto"
                >
                    <div className={"text-xl font-bold"}>
                        {formatNumber(worldNodeDetails?.extractionRate!)}
                    </div>
                    <div className="ml-2 text-gray-400 text-xs leading-8">
                        per min
                    </div>
                </FieldWrapper>
                <FieldWrapper
                    label="Max Extraction Rate"
                    className="col-auto"
                >
                    <div className={"text-xl font-bold"}>
                        {formatNumber(currentExtractor.maxExtractionRate)}
                    </div>
                    <div className="ml-2 text-gray-400 text-xs leading-8">
                        per min
                    </div>
                </FieldWrapper>
                <FieldWrapper
                    label="New Extraction Rate"
                    className="col-span-2"
                >
                    <input
                        type="text"
                        className="p-2 pr-0 w-16 text-right rounded-l border border-r-0 border-solid bg-gray-800 border-gray-600 text-gray-200 focus:outline-none outline-none"
                        onChange={(a) =>
                            setExtractionRate(parseFloat(a.target.value))
                        }
                    />
                    <span className="p-2 text-xs leading-6 rounded-r border border-l-0 border-solid bg-gray-800 border-gray-600 text-gray-400">
                        per min
                    </span>
                </FieldWrapper>
                {errorMessages != null && (
                    <div className="col-span-2">
                        {errorMessages.map((message) => {
                            return (
                                <p
                                    key={message}
                                    className="mb-2 text-red-600"
                                >
                                    {message}
                                </p>
                            );
                        })}
                    </div>
                )}
                <div className="col-span-2">
                    <Button
                        variant="secondary"
                        className="py-2 px-3 rounded-r mr-3"
                        onClick={() => navigate(`/nodes/${nodeId}`)}
                    >
                        Cancel
                    </Button>
                    <Button
                        className="py-2 px-3 rounded-r"
                        onClick={() => {
                            if (extractionRate == null) {
                                setErrorMessages(["Enter an extraction rate."]);
                                return;
                            }

                            increaseWorldNodeExtractionRateMutation.mutate(
                                {
                                    nodeId: nodeId!,
                                    extractionRate: extractionRate!,
                                },
                                {
                                    onSuccess: () =>
                                        navigate(`/nodes/${nodeId}`),
                                    onError: (error) =>
                                        setErrorMessages(
                                            (error as ErrorResponse).messages
                                        ),
                                }
                            );
                        }}
                    >
                        Increase
                    </Button>
                </div>
            </div>
        </ContentLayout>
    );
};
