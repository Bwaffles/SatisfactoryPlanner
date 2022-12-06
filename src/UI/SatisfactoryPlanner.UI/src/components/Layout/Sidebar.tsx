import * as React from "react";
import { useAuth0 } from "@auth0/auth0-react";

import SidebarNavLink from "./SidebarNavLink";
import UserDropdown from "../UserDropdown";

const Sidebar = () => {
    const { isAuthenticated, loginWithRedirect } = useAuth0();

    return (
        <nav>
            <div className="sticky top-0 left-0 w-60 h-screen shadow-md bg-gray-800 px-3 flex flex-col justify-between">
                <div className="mt-3">
                    <SidebarNavLink
                        to="/"
                        text="Home"
                        authenticated={false}
                    />
                    <SidebarNavLink
                        to="/resources"
                        text="Resources"
                        authenticated={true}
                    />
                </div>
                <div className="mb-3">
                    {isAuthenticated ? (
                        <UserDropdown />
                    ) : (
                        <button
                            className="w-full flex items-center py-4 px-6 overflow-hidden text-white text-ellipsis whitespace-nowrap rounded hover:bg-sky-800"
                            onClick={() => loginWithRedirect()}
                        >
                            Log In
                        </button>
                    )}
                </div>
            </div>
        </nav>
    );
};

export default Sidebar;
