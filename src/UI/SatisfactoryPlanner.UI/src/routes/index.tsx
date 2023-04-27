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
const { ResourceDetails } = lazyImport(
    () => import("../pages/ResourceDetails"),
    "ResourceDetails"
);
const { WorldNodeDetails } = lazyImport(
    () => import("../pages/WorldNodeDetails"),
    "WorldNodeDetails"
);
const { IncreaseWorldNodeExtractionRate } = lazyImport(
    () => import("../pages/IncreaseWorldNodeExtractionRate"),
    "IncreaseWorldNodeExtractionRate"
);
const { LoginRedirect } = lazyImport(
    () => import("../pages/LoginRedirect"),
    "LoginRedirect"
);
const { Login } = lazyImport(() => import("../pages/Login"), "Login");
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
                ></Route>
                <Route
                    path="resources/:resourceId"
                    element={
                        isAuthenticated ? <ResourceDetails /> : <AutoLogin />
                    }
                ></Route>
                <Route
                    path="nodes/:nodeId"
                    element={
                        isAuthenticated ? <WorldNodeDetails /> : <AutoLogin />
                    }
                ></Route>
                // TODO I'd like to think about nested routes but for now, new
                page for simplicity to move forward
                <Route
                    path="nodes/:nodeId/increase-extraction-rate"
                    element={
                        isAuthenticated ? (
                            <IncreaseWorldNodeExtractionRate />
                        ) : (
                            <AutoLogin />
                        )
                    }
                ></Route>
                <Route
                    path="/loginError"
                    element={<LoginError />}
                />
                <Route
                    path="*"
                    element={<NoMatch />}
                />
            </Route>
            <Route
                path="/loginRedirect"
                element={<LoginRedirect />}
            />
            <Route
                path="/login"
                element={<Login />}
            />
        </Routes>
    );
};
