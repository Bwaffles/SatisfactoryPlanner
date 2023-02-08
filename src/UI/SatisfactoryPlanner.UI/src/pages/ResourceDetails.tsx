import React from "react";
import { useParams } from "react-router-dom";

import Doggo from "../assets/Lizard_Doggo.png";
import { ContentLayout } from "../components/Layout/ContentLayout";
import { useGetResourceDetails } from "../features/resources/api/getResourceDetails";

export const ResourceDetails = () => {
    const { resourceId } = useParams();
    const {
        isError,
        data: resourceDetails,
        error,
    } = useGetResourceDetails(resourceId!);

    if (isError) {
        return <span>Error: {(error as Error).message}</span>;
    }

    var description = resourceDetails!.description.split("\\r\\n");
    var nodes = [{ name: "Node 1" }, { name: "Node 2" }, { name: "Node 3" }];
    return (
        <ContentLayout title={resourceDetails!.name}>
            <h2 className="text-xl font-bold mb-6">Resource Details</h2>
            <div className="flex flex-col gap-4 mb-10 py-6 px-10 bg-gray-800 rounded">
                <div className="flex flex-row gap-4">
                    <img
                        className="h-40 w-40 text-center"
                        alt="Icon Image"
                        src={Doggo}
                    ></img>
                    <div>
                        {description.map((line) => {
                            return <p className="mb-4">{line}</p>;
                        })}
                    </div>
                </div>
            </div>
            <h2 className="text-xl font-bold mb-6">Nodes</h2>
            {nodes.map((node) => {
                return (
                    <div className="flex flex-row gap-4 mb-4 py-6 px-10 bg-gray-800 rounded">
                        {node.name}
                    </div>
                );
            })}
        </ContentLayout>
    );
};
