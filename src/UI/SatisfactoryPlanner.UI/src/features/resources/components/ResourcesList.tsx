import React from "react";

import Doggo from "../../../assets/Lizard_Doggo.png";
import { useGetResources } from "../api/getResources";

export const ResourcesList = () => {
    const { isError, data: resources, error } = useGetResources();

    if (isError) {
        return <span>Error: {(error as Error).message}</span>;
    }

    var resourceList = resources?.map((resource) => {
        return (
            <div
                key={resource.id}
                className="flex flex-col items-center justify-between h-60 w-60 py-4 px-5 bg-gray-800 rounded hover:bg-sky-900 cursor-pointer"
            >
                <div className="text-center text-lg font-bold">
                    {resource.name}
                </div>
                <img
                    className="h-28 w-28 text-center"
                    alt="Icon Image"
                    src={Doggo}
                ></img>
                <div className="flex flex-row justify-between items-end w-full">
                    <div className="flex flex-row gap-2 items-end">
                        <div className="text-right">
                            <div className="text-right text-xs text-gray-400">
                                Extracted
                            </div>
                            <span className="text-right text-lg">70,380</span>
                        </div>
                        <span className="text-2xl font-semibold">/</span>
                        <div>
                            <div className="text-right text-xs text-gray-400">
                                Total
                            </div>
                            <span className="text-lg">70,380</span>
                        </div>
                    </div>
                    <span className="bg-gray-500 w-px h-full"></span>
                    <span className="text-lg">100%</span>
                </div>
            </div>
        );
    });

    return (
        <div className="flex flex-row flex-wrap gap-4 my-6">{resourceList}</div>
    );
};