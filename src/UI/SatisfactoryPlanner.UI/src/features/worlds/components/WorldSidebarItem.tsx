import * as React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faGlobeAmericas } from "@fortawesome/free-solid-svg-icons";

import useUser from "providers/user-provider";

export const WorldSidebarItem = () => {
  const { world } = useUser();

  if (world) {
    return (
      <div className="flex items-center p-4 overflow-hidden text-white text-ellipsis whitespace-nowrap rounded hover:bg-sky-950 cursor-pointer select-none">
        <FontAwesomeIcon icon={faGlobeAmericas} size="xl" className="mr-2" />
        <div>
          <div className="font-bold">Current World</div>
          <div className="text-muted-foreground text-sm font-semibold">
            {world.name}
          </div>
        </div>
      </div>
    );
  }

  return null;
};
