import React, { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faRightFromBracket } from "@fortawesome/free-solid-svg-icons";
import { useAuth0 } from "@auth0/auth0-react";
import { NavLink } from "react-router-dom";

const UserDropdown = () => {
    const [isOpen, setIsOpen] = useState<boolean>(false);

    const { isAuthenticated, logout, user } = useAuth0();

    function openMenu() {
        setIsOpen(true);
    }

    function closeMenu() {
        setIsOpen(false);
    }

    if (!isAuthenticated) return null;

    // TODO split this into smaller components
    return (
        <div className="relative">
            <div
                className="flex items-center p-4 overflow-hidden text-white text-ellipsis whitespace-nowrap rounded hover:bg-sky-800 cursor-pointer select-none"
                onClick={isOpen ? closeMenu : openMenu}
            >
                <img
                    className="w-7 h-7 mr-2 rounded-full inline"
                    src={user?.picture}
                    alt="profile pic"
                />
                {user?.name}
            </div>
            {isOpen && (
                <div className="fixed bottom-0 left-60 w-48 p-4 flex flex-col justify-between bg-gray-700 rounded-tr border-t border-r border-gray-600">
                    <div className="mb-4 text-white font-bold underline decoration-white">
                        Settings
                    </div>
                    <div className="mb-3 pb-3 flex flex-col">
                        <NavLink
                            className="mb-4"
                            to="/profile"
                            onClick={closeMenu}
                        >
                            Profile
                        </NavLink>
                        <div
                            className="flex justify-between items-center cursor-pointer select-none"
                            onClick={() =>
                                logout({ returnTo: window.location.origin })
                            }
                        >
                            Log Out
                            <FontAwesomeIcon icon={faRightFromBracket} />
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default UserDropdown;
