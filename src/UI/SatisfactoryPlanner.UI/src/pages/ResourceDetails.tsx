import React from "react";
import { useParams } from "react-router-dom";

import Doggo from "@/assets/Lizard_Doggo.png";
import { ContentLayout } from "@/components/Layout/ContentLayout";
import { WorldNodeList } from "@/features/worldNodes/components/WorldNodeList";
import { useGetResourceDetails } from "@/features/resources/api/getResourceDetails";

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
    return (
        <ContentLayout title={resourceDetails!.name}>
            <h2 className="text-xl font-bold mb-6">Resource Details</h2>
            <div className="flex flex-col gap-4 mb-10 py-6 px-10 bg-gray-800 rounded">
                <div className="flex flex-row gap-4">
                    <img
                        className="h-40 w-40 text-center"
                        alt="Icon"
                        src={Doggo}
                    ></img>
                    <div>
                        {description.map((line) => {
                            return (
                                <p
                                    key={line}
                                    className="mb-4"
                                >
                                    {line}
                                </p>
                            );
                        })}
                    </div>
                </div>
            </div>
            <WorldNodeList resourceId={resourceId!}></WorldNodeList>
        </ContentLayout>
    );
};
