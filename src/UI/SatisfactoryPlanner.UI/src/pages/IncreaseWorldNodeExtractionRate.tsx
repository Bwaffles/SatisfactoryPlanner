import React, { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import * as z from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";

import { ContentLayout } from "../components/Layout/ContentLayout";
import { useGetWorldNodeDetails } from "../features/worldNodes/api/getWorldNodeDetails";
import { formatNumber } from "../utils/format";
import { Button } from "../components/Elements/Button";
import {
    IncreaseWorldNodeExtractionRateRequest,
    useIncreaseWorldNodeExtractionRate,
} from "../features/worldNodes/api/increaseWorldNodeExtractionRate";
import { ErrorResponse } from "../lib/axios";
import { FieldWrapper } from "../components/Elements/Form/FieldWrapper";

export const IncreaseWorldNodeExtractionRate = () => {
    const { nodeId } = useParams();
    const [errorMessages, setErrorMessages] = useState<string[] | null>(null);
    const navigate = useNavigate();
    const {
        isError,
        data: worldNodeDetails,
        error,
    } = useGetWorldNodeDetails(nodeId!);
    const increaseWorldNodeExtractionRateMutation =
        useIncreaseWorldNodeExtractionRate();

    const currentExtractor = worldNodeDetails!.availableExtractors.find(
        (extractor) => extractor.id === worldNodeDetails!.extractorId
    )!;

    const schema = z.object({
        extractionRate: z
            .number({
                invalid_type_error: "Enter the new extraction rate as a number",
            })
            .multipleOf(
                0.0001,
                "Extraction rate can only have up to 4 decimals of precision"
            )
            .gt(
                worldNodeDetails!.extractionRate,
                `Enter an extraction rate higher than the current extraction rate of ${
                    worldNodeDetails!.extractionRate
                }`
            )
            .lte(
                currentExtractor!.maxExtractionRate,
                `This node can't handle a rate that high. The max extraction rate is ${
                    currentExtractor!.maxExtractionRate
                }`
            ),
    });

    const methods = useForm<IncreaseWorldNodeExtractionRateRequest["data"]>({
        resolver: zodResolver(schema),
    });

    if (isError) {
        return <span>Error: {(error as Error).message}</span>;
    }

    return (
        <ContentLayout title="Increase Extraction Rate">
            <form
                id="increase-extraction-rate"
                onSubmit={methods.handleSubmit((values) => {
                    increaseWorldNodeExtractionRateMutation.mutate(
                        {
                            nodeId: nodeId!,
                            data: values,
                        },
                        {
                            onSuccess: () => navigate(`/nodes/${nodeId}`),
                            onError: (error) =>
                                setErrorMessages(
                                    (error as ErrorResponse).messages
                                ),
                        }
                    );
                })}
            >
                <div className="p-6 bg-gray-800 rounded w-1/2 grid grid-cols-2 gap-y-6">
                    <FieldWrapper
                        label="Current Extraction Rate"
                        className="col-auto"
                    >
                        <div className="flex">
                            <div className={"text-xl font-bold"}>
                                {formatNumber(
                                    worldNodeDetails?.extractionRate!
                                )}
                            </div>
                            <div className="ml-2 text-gray-400 text-xs leading-8">
                                per min
                            </div>
                        </div>
                    </FieldWrapper>
                    <FieldWrapper
                        label="Max Extraction Rate"
                        className="col-auto"
                    >
                        <div className="flex">
                            <div className={"text-xl font-bold"}>
                                {formatNumber(
                                    currentExtractor.maxExtractionRate
                                )}
                            </div>
                            <div className="ml-2 text-gray-400 text-xs leading-8">
                                per min
                            </div>
                        </div>
                    </FieldWrapper>
                    <FieldWrapper
                        label="New Extraction Rate"
                        className="col-span-2"
                        error={methods.formState.errors?.extractionRate}
                    >
                        <div className="flex">
                            <input
                                type="text"
                                className="p-2 pr-0 w-24 text-right rounded-l border border-r-0 border-solid bg-gray-800 border-gray-600 text-gray-200 focus:outline-none outline-none"
                                {...methods.register("extractionRate", {
                                    valueAsNumber: true,
                                })}
                            />
                            <span className="p-2 text-xs leading-6 rounded-r border border-l-0 border-solid bg-gray-800 border-gray-600 text-gray-400">
                                per min
                            </span>
                        </div>
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
                            type="submit"
                        >
                            Increase
                        </Button>
                    </div>
                </div>
            </form>
        </ContentLayout>
    );
};
