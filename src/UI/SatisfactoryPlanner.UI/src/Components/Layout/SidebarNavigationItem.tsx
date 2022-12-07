import React from "react";
import { NavLink, To } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { IconDefinition } from "@fortawesome/free-solid-svg-icons";

export interface SidebarNavigationItemProps {
    name: string;
    to: To;
    isProtected: boolean;
    icon: IconDefinition;
}

const SidebarNavigationItem = ({
    to,
    name,
    isProtected,
    icon,
}: SidebarNavigationItemProps) => {
    const className = ({ isActive }: { isActive: boolean }) => {
        const baseClasses =
            "flex items-center p-4 overflow-hidden text-white text-ellipsis whitespace-nowrap rounded hover:bg-sky-800";
        return isActive
            ? `${baseClasses} bg-gray-700 font-bold`
            : `${baseClasses}`;
    };

    const { isAuthenticated } = useAuth0();

    if (isProtected && !isAuthenticated) return null;

    return (
        <NavLink
            to={to}
            className={className}
        >
            <FontAwesomeIcon
                icon={icon}
                size="xl"
                className="mr-2"
            />
            {name}
        </NavLink>
    );
};

export default SidebarNavigationItem;
