import * as React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faGlobeAmericas } from "@fortawesome/free-solid-svg-icons";

import { useCurrentPioneerWorlds } from "../api/getCurrentPioneerWorlds";
import storage from "../../../utils/storage";

export const WorldSidebarItem = () => {
    const { data } = useCurrentPioneerWorlds();

    if (data) {
        // TODO need better support for the "current world" instead of just getting index 0--will add when you can create more worlds
        const currentWorld = data[0];
        storage.setWorldId(currentWorld.id);

        return (
            <div className="flex items-center p-4 overflow-hidden text-white text-ellipsis whitespace-nowrap rounded hover:bg-sky-800 cursor-pointer select-none">
                <FontAwesomeIcon
                    icon={faGlobeAmericas}
                    size="xl"
                    className="mr-2"
                />
                <div>
                    <div className="font-bold">Current World</div>
                    <div className="text-gray-400 text-sm font-semibold">
                        {currentWorld.name}
                    </div>
                </div>
            </div>
        );
    }

    return null;
};
