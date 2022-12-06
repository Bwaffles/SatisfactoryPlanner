import React from "react";
import { Outlet } from "react-router-dom";

import Sidebar from "./Sidebar";

const MainLayout = () => {
    return (
        <div className="flex min-h-screen bg-gray-900 text-white">
            <Sidebar />
            <div className="py-4 px-10 w-full h-full">
                <main>
                    <Outlet />
                </main>
            </div>
        </div>
    );
};

export default MainLayout;
