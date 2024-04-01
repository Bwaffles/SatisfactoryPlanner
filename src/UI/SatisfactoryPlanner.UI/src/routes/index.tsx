import React from "react";
import { Routes, Route } from "react-router-dom";

import { MainLayout } from "@/components/Layout";
import { Resources } from "@/pages/Resources";
import { ResourceDetails } from "@/pages/ResourceDetails";
import { Home } from "@/pages/Home";
import { DecreaseWorldNodeExtractionRate } from "@/pages/DecreaseWorldNodeExtractionRate";
import { IncreaseWorldNodeExtractionRate } from "@/pages/IncreaseWorldNodeExtractionRate";
import { Login } from "@/pages/Login";
import { LoginError } from "@/pages/LoginError";
import { LoginRedirect } from "@/pages/LoginRedirect";
import { NoMatch } from "@/pages/NoMatch";
import { Profile } from "@/pages/Profile";
import { WorldNodeDetails } from "@/pages/WorldNodeDetails";

export const AppRoutes = () => {
    return (
        <Routes>
            <Route element={<MainLayout />}>
                <Route
                    index
                    element={<Home />}
                />
                <Route
                    path="profile"
                    element={<Profile />}
                />
                <Route
                    path="resources"
                    element={<Resources />}
                ></Route>
                <Route
                    path="resources/:resourceId"
                    element={<ResourceDetails />}
                ></Route>
                <Route
                    path="nodes/:nodeId"
                    element={<WorldNodeDetails />}
                ></Route>
                <Route
                    path="nodes/:nodeId/increase-extraction-rate"
                    element={<IncreaseWorldNodeExtractionRate />}
                ></Route>
                <Route
                    path="nodes/:nodeId/decrease-extraction-rate"
                    element={<DecreaseWorldNodeExtractionRate />}
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
