import React from 'react';
import { Link, To } from "react-router-dom";

interface NavItemProps {
    to: To,
    text: string
}

const NavItem = ({ to, text }: NavItemProps) => {
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