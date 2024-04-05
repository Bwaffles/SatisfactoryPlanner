import * as React from "react";
import { useAuth0 } from "@auth0/auth0-react";

import UserDropdown from "./UserDropdown";
import { SidebarNavigation } from "./SidebarNavigation";
import { WorldSidebarItem } from "features/worlds";

const Sidebar = () => {
  const { isAuthenticated, loginWithRedirect } = useAuth0();

  return (
    <nav className="flex flex-col shrink-0 gap-3 sticky top-0 left-0 w-60 h-screen p-3 bg-gray-800">
      <div
        className="text-4xl text-center text-white"
        style={{ fontFamily: "FFFTusj" }}
      >
        Satisfactory Planner
      </div>
      <div className="flex flex-col grow gap-2">
        <SidebarNavigation />
      </div>
      {isAuthenticated ? (
        <>
          <WorldSidebarItem />
          <UserDropdown />
        </>
      ) : (
        <button
          className="flex items-center w-full py-4 px-4 overflow-hidden text-white text-ellipsis whitespace-nowrap rounded hover:bg-sky-800"
          onClick={() => loginWithRedirect()}
        >
          Log In
        </button>
      )}
    </nav>
  );
};

export default Sidebar;
