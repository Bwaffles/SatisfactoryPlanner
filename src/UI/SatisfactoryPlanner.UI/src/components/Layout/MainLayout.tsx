import * as React from "react";
import { Outlet } from "react-router-dom";

import { Spinner } from "components/Elements/Spinner";
import Sidebar from "./Sidebar";
import { UserProvider } from "providers/user-provider";

export const MainLayout = () => {
  return (
    <UserProvider>
      <div className="flex min-h-screen bg-background text-foreground">
        <Sidebar />
        <main className="w-full h-full min-h-screen">
          <React.Suspense
            fallback={
              <div className="flex items-center justify-center w-full h-full min-h-screen">
                <Spinner size="xl" />
              </div>
            }
          >
            <Outlet />
          </React.Suspense>
        </main>
      </div>
    </UserProvider>
  );
};
