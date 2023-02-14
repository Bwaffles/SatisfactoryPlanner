import React from "react";
import { formatNumber } from "../../../utils/format";

import { useGetNodes } from "../api/getNodes";
import { Node } from "../types";

type NodeListProps = {
    resourceId: string;
};

export const NodeList = ({ resourceId }: NodeListProps) => {
    const { isError, data: nodes, error } = useGetNodes(resourceId);

    if (isError) {
        return <span>Error: {(error as Error).message}</span>;
    }

    const nodesByBiome = nodes!.reduce<Record<string, Node[]>>(
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

                var resourcesExtracted = 0;
                biomeNodes.forEach((node) => {
                    resourcesExtracted += node.amountToExtract;
                });
                var totalResources = 0;
                biomeNodes.forEach((node) => {
                    totalResources += node.totalResources;
                });

                return (
                    <div className="mb-4 p-6 bg-gray-800 rounded">
                        <div className="flex flex-col mb-6">
                            <h3 className="text-lg font-bold mb-4">{biome}</h3>
                            <div className="flex">
                                <div className="w-36">
                                    <div className="text-gray-400">
                                        Nodes Tapped
                                    </div>
                                    <div className="text-xl font-bold">
                                        {nodesTapped} / {numberOfNodes}
                                    </div>
                                </div>
                                <div className="w-36">
                                    <div className="text-gray-400">
                                        Amount Extracted
                                    </div>
                                    <div className="text-xl font-bold">
                                        {formatNumber(resourcesExtracted)}
                                        {" / "}
                                        {formatNumber(totalResources)}
                                    </div>
                                </div>
                            </div>
                        </div>

                        {biomeNodes.map((node) => {
                            var purityTextColor =
                                node.purity == "Pure"
                                    ? "text-green-600"
                                    : node.purity == "Normal"
                                    ? "text-yellow-600"
                                    : node.purity == "Impure"
                                    ? "text-red-600"
                                    : "";

                            return (
                                <div className="flex flex-row items-center mb-4 py-6 px-6 bg-gray-700 rounded">
                                    <div className="py-4 mr-6 w-16 text-3xl text-center font-bold bg-sky-800 rounded">
                                        {node.number}
                                    </div>
                                    <div className="w-24">
                                        <div className="text-gray-400">
                                            Purity
                                        </div>
                                        <div
                                            className={
                                                "text-xl font-bold " +
                                                purityTextColor
                                            }
                                        >
                                            {node.purity}
                                        </div>
                                    </div>
                                    <div className="w-24">
                                        <div className="text-gray-400">
                                            Tapped
                                        </div>
                                        <div className="text-xl font-bold">
                                            {node.isTapped ? "Yes" : "No"}
                                        </div>
                                    </div>
                                    <div className="w-36">
                                        <div className="text-gray-400">
                                            Amount Extracted
                                        </div>
                                        <div className="text-xl font-bold">
                                            {formatNumber(node.amountToExtract)}
                                            {" / "}
                                            {formatNumber(node.totalResources)}
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
