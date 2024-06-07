import React from "react";
import { useNavigate } from "react-router-dom";

import { formatNumber } from "utils/format";

import { useGetWorldNodes } from "../api/getWorldNodes";
import { WorldNode } from "../types";
import {
  Card,
  CardActionArea,
  CardContent,
  CardHeader,
  CardTitle,
} from "components/Elements/Card/Card";
import { cn } from "utils";

type WorldNodeListProps = {
  resourceId: string;
};

export const WorldNodeList = ({ resourceId }: WorldNodeListProps) => {
  const { isError, data: response, error } = useGetWorldNodes(resourceId);
  const navigate = useNavigate();

  const handleNodeClick = (nodeId: string) => {
    navigate(`/nodes/${nodeId}`);
  };

  if (isError) {
    return <span>Error: {(error as Error).message}</span>;
  }

  const worldNodes = response!.data.worldNodes;
  const nodesByBiome = worldNodes!.reduce<Record<string, WorldNode[]>>(
    (group, node) => {
      const { biome } = node;
      group[biome] = group[biome] ?? [];
      group[biome].push(node);

      return group;
    },
    {}
  );

  return (
    <div className="flex flex-col gap-4">
      {Object.keys(nodesByBiome).map((biome) => {
        var biomeNodes = nodesByBiome[biome];

        var nodesTapped = 0;
        biomeNodes.forEach((node) => {
          nodesTapped += node.isTapped ? 1 : 0;
        });

        var numberOfNodes = biomeNodes.length;

        var currentBiomeExtractionRate = 0;
        biomeNodes.forEach((node) => {
          currentBiomeExtractionRate += node.extractionRate;
        });
        var maxBiomeExtractionRate = 0;
        biomeNodes.forEach((node) => {
          maxBiomeExtractionRate += node.maxExtractionRate;
        });

        return (
          <Card key={biome}>
            <CardHeader>
              <CardTitle>{biome}</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="flex flex-col mb-6">
                <div className="flex gap-10">
                  <div>
                    <div className="text-muted-foreground">Nodes Tapped</div>
                    <div className="text-lg font-bold">
                      {nodesTapped} / {numberOfNodes}
                    </div>
                  </div>
                  <div>
                    <div className="text-muted-foreground">Extraction Rate</div>
                    <div className="text-lg font-bold">
                      {formatNumber(currentBiomeExtractionRate)}
                      {" / "}
                      {formatNumber(maxBiomeExtractionRate)}
                      <span className="ml-2 text-muted-foreground text-xs">
                        per min
                      </span>
                    </div>
                  </div>
                </div>
              </div>

              <div className="flex flex-col gap-4">
                {biomeNodes.map((node) => {
                  var purityTextColor =
                    node.purity === "Pure"
                      ? "text-green-600"
                      : node.purity === "Normal"
                      ? "text-yellow-600"
                      : node.purity === "Impure"
                      ? "text-red-600"
                      : "";

                  return (
                    <Card key={node.id}>
                      <CardActionArea
                        onClick={() => {
                          handleNodeClick(node.id);
                        }}
                      >
                        <CardContent className="py-4 flex flex-row items-end">
                          <div className="py-3 mr-6 w-14 text-2xl text-center font-bold bg-sky-900 rounded">
                            {node.number}
                          </div>
                          <div className="w-24">
                            <div className="text-muted-foreground">Purity</div>
                            <div
                              className={cn(
                                "text-lg font-bold",
                                purityTextColor
                              )}
                            >
                              {node.purity}
                            </div>
                          </div>
                          <div className="w-24">
                            <div className="text-muted-foreground">Tapped</div>
                            <div className="text-lg font-bold">
                              {node.isTapped ? "Yes" : "No"}
                            </div>
                          </div>
                          <div className="w-48">
                            <div className="text-muted-foreground">
                              Extraction Rate
                            </div>
                            <div className="text-lg font-bold">
                              {formatNumber(node.extractionRate)}
                              {" / "}
                              {formatNumber(node.maxExtractionRate)}
                              <span className="ml-2 text-muted-foreground text-xs">
                                per min
                              </span>
                            </div>
                          </div>
                        </CardContent>
                      </CardActionArea>
                    </Card>
                  );
                })}
              </div>
            </CardContent>
          </Card>
        );
      })}
    </div>
  );
};
