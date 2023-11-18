import * as React from "react";
import { faHome, faPersonDigging } from "@fortawesome/free-solid-svg-icons";

import SidebarNavigationItem, {
    SidebarNavigationItemProps,
} from "./SidebarNavigationItem";

export const SidebarNavigation = () => {
    const navitationItems = [
        { name: "Home", to: "/", icon: faHome, isProtected: false },
        {
            name: "Resources",
            to: "/resources",
            icon: faPersonDigging,
            isProtected: true,
        },
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
