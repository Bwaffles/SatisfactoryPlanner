import React from "react";
import { useParams } from "react-router-dom";

import { ContentLayout } from "components/Layout/ContentLayout";
import { useGetWorldNodeDetails } from "features/worldNodes/api/getWorldNodeDetails";
import { TappedWorldNodeView } from "features/worldNodes/components/TappedWorldNodeView";
import { UntappedWorldNodeView } from "features/worldNodes/components/UntappedWorldNodeView";
import { FieldWrapper } from "components/Elements/Form/FieldWrapper";
import { formatNumber } from "utils/format";
import { H2 } from "components/Typography";
import { Card, CardContent } from "components/Elements/Card/Card";

export const WorldNodeDetails = () => {
  const { nodeId } = useParams();
  const {
    isError,
    data: worldNodeDetails,
    error,
  } = useGetWorldNodeDetails(nodeId!);

  if (isError) {
    return <span>Error: {(error as Error).message}</span>;
  }

  var nodeName = `${worldNodeDetails!.resourceName} - ${
    worldNodeDetails!.biome
  } ${worldNodeDetails!.number}`;

  var purityTextColor =
    worldNodeDetails!.purity === "Pure"
      ? "text-green-600"
      : worldNodeDetails!.purity === "Normal"
      ? "text-yellow-600"
      : worldNodeDetails!.purity === "Impure"
      ? "text-red-600"
      : "";

  const currentExtractor = worldNodeDetails!.availableExtractors.find(
    (extractor) => extractor.id === worldNodeDetails!.extractorId
  )!;

  const fastestExtractor =
    worldNodeDetails!.availableExtractors[
      worldNodeDetails!.availableExtractors.length - 1
    ];

  return (
    <ContentLayout title={nodeName}>
      <H2>Node Details</H2>
      <Card className="w-fit">
        <CardContent className="flex flex-wrap gap-x-12 gap-y-4 py-6">
          <FieldWrapper label="Purity">
            <div className={"text-lg font-bold " + purityTextColor}>
              {worldNodeDetails!.purity}
            </div>
          </FieldWrapper>

          {currentExtractor !== undefined && (
            <FieldWrapper label="Max Extraction Rate with Current Extractor">
              <div className="flex">
                <div className={"text-lg font-bold"}>
                  {formatNumber(currentExtractor.maxExtractionRate)}
                </div>
                <div className="ml-2 text-muted-foreground text-xs leading-8">
                  per min
                </div>
              </div>
            </FieldWrapper>
          )}
          <FieldWrapper label="Max Extraction Rate with Fastest Extractor">
            <div className="flex">
              <div className={"text-lg font-bold"}>
                {formatNumber(fastestExtractor.maxExtractionRate)}
              </div>
              <div className="ml-2 text-muted-foreground text-xs leading-8">
                per min
              </div>
            </div>
          </FieldWrapper>
        </CardContent>
      </Card>

      <H2 className="mt-6">Extraction Details</H2>
      {worldNodeDetails!.isTapped ? (
        <TappedWorldNodeView worldNodeDetails={worldNodeDetails!} />
      ) : (
        <UntappedWorldNodeView worldNodeDetails={worldNodeDetails!} />
      )}
    </ContentLayout>
  );
};
