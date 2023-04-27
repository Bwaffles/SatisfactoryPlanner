import React, { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import { ContentLayout } from "../components/Layout/ContentLayout";
import { useGetWorldNodeDetails } from "../features/worldNodes/api/getWorldNodeDetails";
import { formatNumber } from "../utils/format";
import { Button } from "../components/Elements/Button";
import { useIncreaseWorldNodeExtractionRate } from "../features/worldNodes/api/increaseWorldNodeExtractionRate";

export const IncreaseWorldNodeExtractionRate = () => {
    const { nodeId } = useParams();
    const [extractionRate, setExtractionRate] = useState<number | null>(null);
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
            <div className="p-6 bg-gray-800 rounded w-1/2">
                <div className="flex mb-6">
                    <div className="w-1/2">
                        <label className="inline-block text-gray-400 mb-2">
                            Current Extraction Rate
                        </label>
                        <div className="flex">
                            <div className={"text-xl font-bold mb-4"}>
                                {formatNumber(
                                    worldNodeDetails?.extractionRate!
                                )}
                            </div>
                            <div className="ml-2 text-gray-400 text-xs leading-8">
                                per min
                            </div>
                        </div>
                    </div>
                    <div className="w-1/2">
                        <label className="inline-block text-gray-400 mb-2">
                            Max Extraction Rate
                        </label>

                        <div className="flex">
                            <div className={"text-xl font-bold mb-4"}>
                                {formatNumber(
                                    currentExtractor.maxExtractionRate
                                )}
                            </div>
                            <div className="ml-2 text-gray-400 text-xs leading-8">
                                per min
                            </div>
                        </div>
                    </div>
                </div>
                <div className="mb-6">
                    <p className="mb-4">Enter your new extraction rate:</p>
                    <div className="relative flex">
                        <input
                            type="text"
                            className="p-2 pr-0 w-16 text-right rounded-l border border-r-0 border-solid bg-gray-800 border-gray-600 text-gray-200 focus:outline-none outline-none"
                            onChange={(a) =>
                                setExtractionRate(parseInt(a.target.value))
                            }
                        />
                        <span className="p-2 text-xs leading-6 rounded-r border border-l-0 border-solid bg-gray-800 border-gray-600 text-gray-400">
                            per min
                        </span>
                    </div>
                </div>
                <div className="flex gap-3">
                    <Button
                        className="py-2 px-3 w-1/4 rounded-r"
                        onClick={() => {
                            if (extractionRate == null) {
                                alert("Enter an extraction rate.");
                            }

                            increaseWorldNodeExtractionRateMutation.mutate(
                                {
                                    nodeId: nodeId!,
                                    extractionRate: extractionRate!,
                                },
                                {
                                    onSuccess: () =>
                                        navigate(`/nodes/${nodeId}`),
                                    onError: () => {
                                        // TODO get problem details working so i can pull the error messages
                                        alert("something went wrong");
                                    },
                                }
                            );
                        }}
                    >
                        Increase
                    </Button>
                    <Button
                        className="py-2 px-3 w-1/4 rounded-r"
                        onClick={() => navigate(`/nodes/${nodeId}`)}
                    >
                        Cancel
                    </Button>
                </div>
            </div>
        </ContentLayout>
    );
};
