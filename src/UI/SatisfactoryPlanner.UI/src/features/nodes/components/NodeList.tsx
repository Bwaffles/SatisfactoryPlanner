import React from "react";

import { useGetNodes } from "../api/getNodes";

type NodeListProps = {
    resourceId: string;
};

export const NodeList = ({ resourceId }: NodeListProps) => {
    const { isError, data: nodes, error } = useGetNodes(resourceId);

    if (isError) {
        return <span>Error: {(error as Error).message}</span>;
    }

    return (
        <>
            <h2 className="text-xl font-bold mb-6">Nodes</h2>
            {nodes!.map((node) => {
                return (
                    <div className="mb-4 py-6 px-10 bg-gray-800 rounded">
                        {node.biome} - {node.id}
                        <p>Purity: {node.purity}</p>
                    </div>
                );
            })}
        </>
    );
};
