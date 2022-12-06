import React from "react";
import { Routes, Route } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

import { lazyImport } from "../utils/lazyimports";
import { MainLayout } from "../components/Layout";

const { AutoLogin } = lazyImport(
    () => import("../pages/AutoLogin"),
    "AutoLogin"
);
const { Home } = lazyImport(() => import("../pages/Home"), "Home");
const { Profile } = lazyImport(() => import("../pages/Profile"), "Profile");
const { Resources } = lazyImport(
    () => import("../pages/Resources"),
    "Resources"
);
const { Callback } = lazyImport(() => import("../pages/Callback"), "Callback");
const { LoginError } = lazyImport(
    () => import("../pages/LoginError"),
    "LoginError"
);
const { NoMatch } = lazyImport(() => import("../pages/NoMatch"), "NoMatch");

export const AppRoutes = () => {
    const { isAuthenticated } = useAuth0();

    return (
        <Routes>
            <Route element={<MainLayout />}>
                <Route
                    index
                    element={<Home />}
                />
                <Route
                    path="profile"
                    element={isAuthenticated ? <Profile /> : <AutoLogin />}
                />
                <Route
                    path="resources"
                    element={isAuthenticated ? <Resources /> : <AutoLogin />}
                />
                <Route
                    path="/callback"
                    element={<Callback />}
                />
                <Route
                    path="/loginError"
                    element={<LoginError />}
                />
                <Route
                    path="*"
                    element={<NoMatch />}
                />
            </Route>
        </Routes>
    );
};
