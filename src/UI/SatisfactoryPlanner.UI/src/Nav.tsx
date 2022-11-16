import * as React from "react";
import Auth from "./Auth/Auth";
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";

interface NavProps {
    auth: Auth;
}

const Nav = ({ auth }: NavProps) => {
    const { isAuthenticated, logout, login } = auth;
    const navigate = useNavigate();

    return (
        <nav>
            <div className="w-60 h-full shadow-md bg-gray-800 px-1">
                <ul>
                    <li>
                        <Link
                            to="/"
                            className="flex items-center py-4 px-6 overflow-hidden text-sm text-white text-ellipsis whitespace-nowrap rounded hover:bg-sky-800 transition duration-100 ease-in-out"
                            data-mdb-ripple="true"
                            data-mdb-ripple-color="dark"
                        >
                            Home
                        </Link>
                    </li>
                    <li>
                        <Link
                            to="/profile"
                            className="flex items-center py-4 px-6 overflow-hidden text-sm text-white text-ellipsis whitespace-nowrap rounded hover:bg-sky-800 transition duration-100 ease-in-out"
                            data-mdb-ripple="true"
                            data-mdb-ripple-color="dark"
                        >
                            Profile
                        </Link>
                    </li>
                    <li>
                        <button
                            onClick={() => isAuthenticated() ? logout(navigate) : login()}
                        >
                            {isAuthenticated() ? "Logout" : "Login"}
                        </button>
                    </li>
                </ul>
            </div >
        </nav >
    );
}

export default Nav;