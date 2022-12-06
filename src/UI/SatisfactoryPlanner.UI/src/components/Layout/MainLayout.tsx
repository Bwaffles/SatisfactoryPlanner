import React from "react";
import { Outlet } from "react-router-dom";

import Nav from "../Nav";

const MainLayout = () => {
    return (
        <div className="flex min-h-screen bg-gray-900 text-white">
            <Nav />
            <div className="py-4 px-10 w-full h-full">
                <main>
                    <Outlet />
                </main>
            </div>
        </div>
    );
};

export default MainLayout;
