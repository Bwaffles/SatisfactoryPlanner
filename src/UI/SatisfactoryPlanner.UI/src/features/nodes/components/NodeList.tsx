import React from "react";

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
                return (
                    <div className="mb-4 py-6 px-6 bg-gray-800 rounded">
                        <h3 className="text-lg font-bold mb-4">{biome}</h3>
                        {nodesByBiome[biome].map((node) => {
                            return (
                                <div className="mb-4 py-6 px-6 bg-gray-700 rounded">
                                    {node.id}
                                    <p>Purity: {node.purity}</p>
                                </div>
                            );
                        })}
                    </div>
                );
            })}
        </>
    );
};
