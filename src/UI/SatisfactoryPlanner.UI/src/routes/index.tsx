import React from "react";
import { Routes, Route } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

import { MainLayout } from "@/components/Layout";
import { Resources } from "@/pages/Resources";
import { ResourceDetails } from "@/pages/ResourceDetails";
import { Home } from "@/pages/Home";
import { AutoLogin } from "@/pages/AutoLogin";
import { DecreaseWorldNodeExtractionRate } from "@/pages/DecreaseWorldNodeExtractionRate";
import { IncreaseWorldNodeExtractionRate } from "@/pages/IncreaseWorldNodeExtractionRate";
import { Login } from "@/pages/Login";
import { LoginError } from "@/pages/LoginError";
import { LoginRedirect } from "@/pages/LoginRedirect";
import { NoMatch } from "@/pages/NoMatch";
import { Profile } from "@/pages/Profile";
import { WorldNodeDetails } from "@/pages/WorldNodeDetails";

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
                    path="nodes/:nodeId/decrease-extraction-rate"
                    element={
                        isAuthenticated ? (
                            <DecreaseWorldNodeExtractionRate />
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
