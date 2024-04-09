import React from "react";
import { useNavigate } from "react-router-dom";

import { formatNumber } from "utils/format";

import { useGetWorldNodes } from "../api/getWorldNodes";
import { WorldNode } from "../types";

type WorldNodeListProps = {
  resourceId: string;
};

export const WorldNodeList = ({ resourceId }: WorldNodeListProps) => {
  const { isError, data: worldNodes, error } = useGetWorldNodes(resourceId);
  const navigate = useNavigate();

  const handleNodeClick = (nodeId: string) => {
    navigate(`/nodes/${nodeId}`);
  };

  if (isError) {
    return <span>Error: {(error as Error).message}</span>;
  }

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
    <>
      <h2 className="text-xl font-bold mb-6">Nodes</h2>
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
          <div key={biome} className="mb-4 p-6 bg-gray-900 rounded">
            <div className="flex flex-col mb-6">
              <h3 className="text-lg font-bold mb-4">{biome}</h3>
              <div className="flex gap-10">
                <div>
                  <div className="text-gray-400">Nodes Tapped</div>
                  <div className="text-xl font-bold">
                    {nodesTapped} / {numberOfNodes}
                  </div>
                </div>
                <div>
                  <div className="text-gray-400">Extraction Rate</div>
                  <div className="text-xl font-bold">
                    {formatNumber(currentBiomeExtractionRate)}
                    {" / "}
                    {formatNumber(maxBiomeExtractionRate)}
                    <span className="ml-2 text-gray-400 text-xs">per min</span>
                  </div>
                </div>
              </div>
            </div>

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
                <div
                  key={node.id}
                  className="flex flex-row items-center mb-4 py-6 px-6 bg-gray-800 rounded hover:bg-gray-800/60 cursor-pointer"
                  onClick={() => {
                    handleNodeClick(node.id);
                  }}
                >
                  <div className="py-4 mr-6 w-16 text-3xl text-center font-bold bg-sky-800 rounded">
                    {node.number}
                  </div>
                  <div className="w-24">
                    <div className="text-gray-400">Purity</div>
                    <div className={"text-xl font-bold " + purityTextColor}>
                      {node.purity}
                    </div>
                  </div>
                  <div className="w-24">
                    <div className="text-gray-400">Tapped</div>
                    <div className="text-xl font-bold">
                      {node.isTapped ? "Yes" : "No"}
                    </div>
                  </div>
                  <div className="w-48">
                    <div className="text-gray-400">Extraction Rate</div>
                    <div className="text-xl font-bold">
                      {formatNumber(node.extractionRate)}
                      {" / "}
                      {formatNumber(node.maxExtractionRate)}
                      <span className="ml-2 text-gray-400 text-xs">
                        per min
                      </span>
                    </div>
                  </div>
                </div>
              );
            })}
          </div>
        );
      })}
    </>
  );
};
