import React from "react";
import { useNavigate } from "react-router-dom";

import Doggo from "assets/Lizard_Doggo.png";

import { useGetResources } from "../api/getResources";
import { formatNumber, formatPercent } from "utils/format";
import {
  Card,
  CardActionArea,
  CardContent,
  CardHeader,
  CardTitle,
} from "components/Elements/Card/Card";
import { Separator } from "components/Elements/Separator/Separator";

export const ResourcesList = () => {
  const { isError, data: resources, error } = useGetResources();
  const navigate = useNavigate();

  const handleResourceClick = (resourceId: string) => {
    navigate(`${resourceId}`);
  };

  if (isError) {
    return <span>Error: {(error as Error).message}</span>;
  }

  var resourceList = resources?.map((resource) => {
    var percentResourceExtractionRate =
      resource.maxExtractionRate === 0
        ? 0
        : resource.extractionRate / resource.maxExtractionRate;

    return (
      <Card key={resource.id}>
        <CardActionArea
          onClick={() => {
            handleResourceClick(resource.id);
          }}
        >
          <CardHeader className="text-center">
            <CardTitle>{resource.name}</CardTitle>
          </CardHeader>
          <CardContent className="flex flex-col items-center justify-between h-52 w-60">
            <img className="h-28 w-28 text-center" alt="Resource" src={Doggo} />
            <div className="flex flex-row justify-between items-end w-full">
              <div className="flex flex-row gap-2 items-end">
                <div className="text-right">
                  <div className="text-xs text-muted-foreground">Extracted</div>
                  <span className="text-lg">
                    {formatNumber(resource.extractionRate)}
                  </span>
                </div>
                <span className="text-2xl font-semibold">/</span>
                <div className="text-right">
                  <div className="text-xs text-muted-foreground">Total</div>
                  <span className="text-lg">
                    {formatNumber(resource.maxExtractionRate)}
                  </span>
                </div>
              </div>
              <Separator orientation="vertical"></Separator>
              <span className="text-lg">
                {formatPercent(percentResourceExtractionRate)}
              </span>
            </div>
          </CardContent>
        </CardActionArea>
      </Card>
    );
  });

  return (
    <div className="flex flex-row flex-wrap gap-4 my-6">{resourceList}</div>
  );
};
