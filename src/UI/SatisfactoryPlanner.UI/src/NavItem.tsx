import React from 'react';
import { Link, To } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

interface NavItemProps {
    to: To,
    text: string,
    authenticated: boolean
}

const NavItem = ({ to, text, authenticated }: NavItemProps) => {
    const { isAuthenticated } = useAuth0();

    if (authenticated && !isAuthenticated)
        return null;

    return (
        <Link
            to={to}
            className="flex items-center py-4 px-6 overflow-hidden text-white text-ellipsis whitespace-nowrap rounded hover:bg-sky-800"
        >
            {text}
        </Link>
    );
}

export default NavItem;