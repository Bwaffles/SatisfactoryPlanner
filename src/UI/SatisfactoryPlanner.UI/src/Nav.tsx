import * as React from "react";
import { useAuth0 } from "@auth0/auth0-react";

import NavItem from "./NavItem";

const Nav = () => {
    const { isAuthenticated, loginWithRedirect, logout } = useAuth0();

    return (
        <nav>
            <div className="w-60 h-screen shadow-md bg-gray-800 px-3 flex flex-col justify-between">
                <div className="mt-3">
                    <NavItem to="/" text="Home" authenticated={false} />
                    <NavItem to="/resources" text="Resources" authenticated={true} />
                </div>
                <div className="mb-3">
                    <NavItem to="/profile" text="Profile" authenticated={true} />
                    <button
                        className="w-full flex items-center py-4 px-6 overflow-hidden text-white text-ellipsis whitespace-nowrap rounded hover:bg-sky-800"
                        onClick={() => isAuthenticated
                            ? logout({ returnTo: window.location.origin })
                            : loginWithRedirect()}
                    >
                        {isAuthenticated ? "Log Out" : "Log In"}
                    </button>
                </div>
            </div>
        </nav >
    );
}

export default Nav;