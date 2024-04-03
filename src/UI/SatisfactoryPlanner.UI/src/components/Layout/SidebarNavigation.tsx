import * as React from "react";
import { faHome, faOilWell, faHammer } from "@fortawesome/free-solid-svg-icons";

import SidebarNavigationItem, {
    SidebarNavigationItemProps,
} from "./SidebarNavigationItem";

export const SidebarNavigation = () => {
    const navitationItems = [
        { name: "Home", to: "/", icon: faHome, isProtected: false },
        { name: "Resources", to: "/resources", icon: faOilWell, isProtected: true },
        { name: "Production Lines", to: "/production-lines", icon: faHammer, isProtected: true },
    ] as SidebarNavigationItemProps[];

    return (
        <>
            {navitationItems.map((item) => (
                <SidebarNavigationItem
                    key={item.name}
                    {...item}
                />
            ))}
        </>
    );
};
