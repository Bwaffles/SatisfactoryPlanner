import React, { useState } from "react";

import { Button } from "components/Elements/Button";
import { FieldWrapper } from "components/Elements/Form/FieldWrapper";
import { formatNumber } from "utils/format";

import { useTapWorldNode } from "../api/tapWorldNode";
import { WorldNodeDetails } from "../api/getWorldNodeDetails";
import { Card, CardContent, CardFooter } from "components/Elements/Card/Card";

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
    <Card className="w-fit">
      <CardContent className="flex flex-col gap-6 p-6">
        <p>Select the extractor to tap the node with:</p>
        <div className="flex flex-wrap gap-12">
          {worldNodeDetails.availableExtractors?.map((extractor) => {
            var selectedExtractorClasses =
              extractor.id === selectedExtractor
                ? "bg-sky-900 border-white"
                : "bg-gray-800 hover:bg-gray-800/70 cursor-pointer border-transparent";

            return (
              <div
                key={extractor.id}
                className={
                  "flex flex-col items-center rounded-lg p-6 w-64 border " +
                  selectedExtractorClasses
                }
                onClick={
                  tapNodeMutation.isLoading
                    ? () => {}
                    : () => {
                        setSelectedExtractor(extractor.id);
                        setValidationMessage(null);
                      }
                }
              >
                <div className="text-lg font-bold mb-3">{extractor.name}</div>
                <FieldWrapper label="Max Extraction Rate" className="col-auto">
                  <div className="flex">
                    <div className={"text-lg font-bold"}>
                      {formatNumber(extractor.maxExtractionRate)}
                    </div>
                    <div className="ml-2 text-muted-foreground text-xs leading-8">
                      per min
                    </div>
                  </div>
                </FieldWrapper>
              </div>
            );
          })}
        </div>
        {validationMessage != null && (
          <div className="text-destructive-error">{validationMessage}</div>
        )}
      </CardContent>
      <CardFooter>
        <Button
          isLoading={tapNodeMutation.isLoading}
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
      </CardFooter>
    </Card>
  );
};
