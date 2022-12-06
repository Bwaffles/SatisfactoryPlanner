import React from "react";
import { NavLink, To } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

interface SidebarNavLinkProps {
    to: To;
    text: string;
    authenticated: boolean;
}

const SidebarNavLink = ({ to, text, authenticated }: SidebarNavLinkProps) => {
    const className = ({ isActive }: { isActive: boolean }) => {
        const baseClasses =
            "flex items-center py-4 px-6 overflow-hidden text-white text-ellipsis whitespace-nowrap rounded hover:bg-sky-800";
        return isActive
            ? `${baseClasses} bg-gray-700 font-bold`
            : `${baseClasses}`;
    };

    const { isAuthenticated } = useAuth0();

    if (authenticated && !isAuthenticated) return null;

    return (
        <NavLink
            to={to}
            className={className}
        >
            {text}
        </NavLink>
    );
};

export default SidebarNavLink;
