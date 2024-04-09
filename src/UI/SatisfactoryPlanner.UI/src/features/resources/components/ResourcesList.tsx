import React from "react";
import { useNavigate } from "react-router-dom";

import Doggo from "assets/Lizard_Doggo.png";

import { useGetResources } from "../api/getResources";
import { formatNumber, formatPercent } from "utils/format";

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
      <div
        key={resource.id}
        className="flex flex-col items-center justify-between h-60 w-60 py-4 px-5 bg-gray-900 rounded hover:bg-gray-900/60 cursor-pointer"
        onClick={() => {
          handleResourceClick(resource.id);
        }}
      >
        <div className="text-center text-lg font-bold">{resource.name}</div>
        <img className="h-28 w-28 text-center" alt="Resource" src={Doggo}></img>
        <div className="flex flex-row justify-between items-end w-full">
          <div className="flex flex-row gap-2 items-end">
            <div className="text-right">
              <div className="text-xs text-muted-foreground">Extracted</div>
              <span className="text-lg">
                {formatNumber(resource.extractionRate)}
              </span>
            </div>
            <span className="text-2xl font-semibold">/</span>
            <div>
              <div className="text-right text-xs text-muted-foreground">
                Total
              </div>
              <span className="text-lg">
                {formatNumber(resource.maxExtractionRate)}
              </span>
            </div>
          </div>
          <span className="bg-gray-500 w-px h-full"></span>
          <span className="text-lg">
            {formatPercent(percentResourceExtractionRate)}
          </span>
        </div>
      </div>
    );
  });

  return (
    <div className="flex flex-row flex-wrap gap-4 my-6">{resourceList}</div>
  );
};
