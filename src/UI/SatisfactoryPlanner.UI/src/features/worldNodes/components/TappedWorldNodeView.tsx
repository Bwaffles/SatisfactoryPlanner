import * as React from "react";
import { useState } from "react";
import { NavigateFunction, useNavigate } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faPlus,
  faMinus,
  faBan,
  faArrowUp,
  faArrowDown,
} from "@fortawesome/free-solid-svg-icons";

import { Button } from "components/Elements/Button";
import { FieldWrapper } from "components/Elements/Form/FieldWrapper";
import { formatNumber } from "utils/format";

import { useDowngradeExtractor } from "../api/downgradeExtractor";
import { useUpgradeExtractor } from "../api/upgradeExtractor";
import { useDismantleExtractor } from "../api/dismantleExtractor";
import {
  AvailableExtractor,
  WorldNodeDetails,
} from "../api/getWorldNodeDetails";
import { Card, CardContent } from "components/Elements/Card/Card";
import { cn } from "utils";

type TappedWorldNodeViewProps = {
  worldNodeDetails: WorldNodeDetails;
};

export const TappedWorldNodeView = ({
  worldNodeDetails,
}: TappedWorldNodeViewProps) => {
  const navigate = useNavigate();
  const downgradeExtractorMutation = useDowngradeExtractor();
  const upgradeExtractorMutation = useUpgradeExtractor();
  const dismantleExtractorMutation = useDismantleExtractor();
  const [selectedExtractor, setSelectedExtractor] = useState<string | null>(
    null
  );

  const currentExtractor = worldNodeDetails.availableExtractors.find(
    (extractor) => extractor.id === worldNodeDetails.extractorId
  )!;

  return (
    <Card className="w-fit">
      <CardContent className="py-6">
        <FieldWrapper label="Extraction Rate" className="mb-10">
          {renderExtractionRate(navigate, worldNodeDetails, currentExtractor)}
        </FieldWrapper>
        <FieldWrapper label="Extractor">
          {renderExtractor(
            worldNodeDetails,
            currentExtractor,
            downgradeExtractorMutation,
            upgradeExtractorMutation,
            dismantleExtractorMutation,
            selectedExtractor,
            setSelectedExtractor
          )}
        </FieldWrapper>
      </CardContent>
    </Card>
  );
};

function renderExtractor(
  worldNodeDetails: WorldNodeDetails,
  currentExtractor: AvailableExtractor,
  downgradeExtractorMutation: any,
  upgradeExtractorMutation: any,
  dismantleExtractorMutation: any,
  selectedExtractor: string | null,
  setSelectedExtractor: React.Dispatch<React.SetStateAction<string | null>>
) {
  return (
    <div className="flex flex-col gap-3">
      <div className="text-lg font-bold">{currentExtractor.name}</div>
      <div className="flex flex-wrap gap-3">
        {worldNodeDetails.availableExtractors.map((extractor) => {
          if (extractor.id === currentExtractor.id) return null;
          var canUpgrade =
            extractor.maxExtractionRate > currentExtractor.maxExtractionRate;
          var canDowngrade =
            extractor.maxExtractionRate < currentExtractor.maxExtractionRate;
          return (
            <div key={extractor.id}>
              {canDowngrade && (
                <Button
                  title="Downgrade to this extractor and decrease your max extraction rate."
                  isLoading={
                    selectedExtractor === extractor.id &&
                    downgradeExtractorMutation.isLoading
                  }
                  onClick={() => {
                    setSelectedExtractor(extractor.id);
                    downgradeExtractorMutation.mutate({
                      nodeId: worldNodeDetails.nodeId,
                      extractorId: extractor.id,
                    });
                  }}
                >
                  <FontAwesomeIcon icon={faArrowDown} className="font-bold" />{" "}
                  Downgrade to {extractor.name}
                </Button>
              )}
              {canUpgrade && (
                <Button
                  title="Upgrade to this extractor and increase your max extraction rate."
                  isLoading={
                    selectedExtractor === extractor.id &&
                    upgradeExtractorMutation.isLoading
                  }
                  onClick={() => {
                    setSelectedExtractor(extractor.id);
                    upgradeExtractorMutation.mutate({
                      nodeId: worldNodeDetails.nodeId,
                      extractorId: extractor.id,
                    });
                  }}
                >
                  <FontAwesomeIcon icon={faArrowUp} /> Upgrade to{" "}
                  {extractor.name}
                </Button>
              )}
            </div>
          );
        })}
        <Button
          variant="destructive"
          title="Dismantle the current extractor and stop tapping this node."
          isLoading={dismantleExtractorMutation.isLoading}
          onClick={() => {
            dismantleExtractorMutation.mutate({
              nodeId: worldNodeDetails.nodeId,
            });
          }}
        >
          <FontAwesomeIcon icon={faBan} /> Dismantle
        </Button>
      </div>
    </div>
  );
}

function renderExtractionRate(
  navigate: NavigateFunction,
  worldNodeDetails: WorldNodeDetails,
  currentExtractor: AvailableExtractor
) {
  const canDecreaseRate = worldNodeDetails.extractionRate > 0;
  const canIncreaseRate =
    worldNodeDetails.extractionRate < currentExtractor.maxExtractionRate;

  return (
    <div className="relative flex">
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
        className={cn(
          "h-10 p-2 pr-0 w-24 text-right border border-solid bg-gray-800 border-gray-600 text-gray-200",
          canDecreaseRate ? "border-x-0" : "border-r-0 rounded-l"
        )}
        value={formatNumber(worldNodeDetails.extractionRate)}
        disabled={true}
      />
      <span
        className={cn(
          "h-10 p-2 text-xs whitespace-nowrap leading-6 border border-solid bg-gray-800 border-gray-600 text-gray-400",
          canIncreaseRate ? "border-x-0" : "border-l-0 rounded-r"
        )}
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
