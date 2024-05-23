import React, { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import * as z from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";

import { Button } from "components/Elements/Button";
import { ContentLayout } from "components/Layout/ContentLayout";
import { FieldWrapper } from "components/Elements/Form/FieldWrapper";
import { useGetWorldNodeDetails } from "features/worldNodes/api/getWorldNodeDetails";
import {
  DecreaseWorldNodeExtractionRateRequest,
  useDecreaseWorldNodeExtractionRate,
} from "features/worldNodes/api/decreaseWorldNodeExtractionRate";
import { ErrorResponse } from "lib/api";
import { formatNumber } from "utils/format";
import { Card, CardContent } from "components/Elements/Card/Card";

export const DecreaseWorldNodeExtractionRate = () => {
  const { nodeId } = useParams();
  const [errorMessages, setErrorMessages] = useState<string[] | null>(null);
  const navigate = useNavigate();
  const {
    isError,
    data: worldNodeDetails,
    error,
  } = useGetWorldNodeDetails(nodeId!);
  const decreaseWorldNodeExtractionRateMutation =
    useDecreaseWorldNodeExtractionRate();

  const schema = z.object({
    extractionRate: z
      .number({
        invalid_type_error: "Enter the new extraction rate as a number",
      })
      .multipleOf(
        0.0001,
        "Extraction rate can only have up to 4 decimals of precision"
      )
      .min(0, `Enter an extraction rate of 0 or more`)
      .max(
        worldNodeDetails!.extractionRate,
        `Enter an extraction rate lower than the current extraction rate of ${
          worldNodeDetails!.extractionRate
        }`
      ),
  });

  const methods = useForm<DecreaseWorldNodeExtractionRateRequest["data"]>({
    resolver: zodResolver(schema),
  });

  if (isError) {
    return <span>Error: {(error as Error).message}</span>;
  }

  const canDecreaseRate = worldNodeDetails?.extractionRate! > 0;

  return (
    <ContentLayout title="Decrease Extraction Rate">
      {canDecreaseRate ? (
        <form
          id="decrease-extraction-rate"
          onSubmit={methods.handleSubmit((values) => {
            decreaseWorldNodeExtractionRateMutation.mutate(
              {
                nodeId: nodeId!,
                data: values,
              },
              {
                onSuccess: () => navigate(`/nodes/${nodeId}`),
                onError: (error) =>
                  setErrorMessages((error as ErrorResponse).messages),
              }
            );
          })}
        >
          <div className="w-1/2 grid grid-cols-2 gap-y-6">
            <FieldWrapper label="Current Extraction Rate" className="col-auto">
              <div className="flex">
                <div className={"text-lg font-bold"}>
                  {formatNumber(worldNodeDetails?.extractionRate!)}
                </div>
                <div className="ml-2 text-muted-foreground text-xs leading-8">
                  per min
                </div>
              </div>
            </FieldWrapper>
            <FieldWrapper
              label="New Extraction Rate"
              labelHtmlFor="extractionRate"
              className="col-span-2"
              error={methods.formState.errors?.extractionRate}
            >
              <div className="flex">
                <input
                  id="extractionRate"
                  type="text"
                  className="p-2 pr-0 w-24 text-right rounded-l border border-r-0 border-solid bg-background border-input text-gray-200 focus:outline-none outline-none"
                  {...methods.register("extractionRate", {
                    valueAsNumber: true,
                  })}
                />
                <span className="p-2 text-xs leading-6 rounded-r border border-l-0 border-solid bg-background border-input text-muted-foreground">
                  per min
                </span>
              </div>
            </FieldWrapper>
            {errorMessages != null && (
              <div className="col-span-2">
                {errorMessages.map((message) => {
                  return (
                    <p key={message} className="mb-2 text-destructive-error">
                      {message}
                    </p>
                  );
                })}
              </div>
            )}
            <div className="col-span-2">
              <Button className="mr-3" type="submit">
                Decrease
              </Button>
              <Button
                variant="outline"
                onClick={() => navigate(`/nodes/${nodeId}`)}
              >
                Cancel
              </Button>
            </div>
          </div>
        </form>
      ) : (
        <Card className="w-1/2">
          <CardContent>
            <div className="p-20 text-center text-lg">
              The extraction rate is already 0 and can't be decreased any
              further.
            </div>
          </CardContent>
        </Card>
      )}
    </ContentLayout>
  );
};
