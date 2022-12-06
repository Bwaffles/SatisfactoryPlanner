import * as React from "react";
import { Outlet } from "react-router-dom";

import { Spinner } from "../../components/Elements/Spinner";
import Sidebar from "./Sidebar";

export const MainLayout = () => {
    return (
        <div className="flex min-h-screen bg-gray-900 text-white">
            <Sidebar />
            <div className="py-4 px-10 w-full h-full min-h-screen">
                <main>
                    <React.Suspense
                        fallback={
                            <div className="flex items-center justify-center -my-4 w-full h-full min-h-screen bg-gray-900">
                                <Spinner size="xl" />
                            </div>
                        }
                    >
                        <Outlet />
                    </React.Suspense>
                </main>
            </div>
        </div>
    );
};
