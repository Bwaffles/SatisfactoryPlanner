import * as React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faGlobeAmericas } from "@fortawesome/free-solid-svg-icons";

export const WorldSidebarItem = () => {
    // TODO need to get this from the api
    const currentWorld = "Starter World";

    return (
        <div className="flex items-center p-4 overflow-hidden text-white text-ellipsis whitespace-nowrap rounded hover:bg-sky-800 cursor-pointer select-none">
            <FontAwesomeIcon
                icon={faGlobeAmericas}
                size="xl"
                className="mr-2"
            />
            <div>
                <div className="font-bold">Current World</div>
                <div className="text-gray-400">{currentWorld}</div>
            </div>
        </div>
    );
};
