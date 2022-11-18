import * as React from "react";
import Auth from "./Auth/Auth";
import NavItem from "./NavItem";

interface NavProps {
    auth: Auth;
}

const Nav = ({ auth }: NavProps) => {
    const { isAuthenticated, logout, login } = auth;

    return (
        <nav>
            <div className="w-60 h-full shadow-md bg-gray-800 px-3 flex flex-col justify-between">
                <div className="mt-3">
                    <NavItem to="/" text="Home" />
                    <NavItem to="/profile" text="Profile" />
                </div>
                <div className="mb-3">
                    <button
                        className="w-full flex items-center py-4 px-6 overflow-hidden text-white text-ellipsis whitespace-nowrap rounded hover:bg-sky-800"
                        onClick={() => isAuthenticated() ? logout() : login()}
                    >
                        {isAuthenticated() ? "Log Out" : "Log In"}
                    </button>
                </div>
            </div>
        </nav >
    );
}

export default Nav;