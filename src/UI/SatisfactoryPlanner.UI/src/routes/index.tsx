import React, { ComponentType } from "react";
import { Routes, Route } from "react-router-dom";
import { withAuthenticationRequired } from "@auth0/auth0-react";

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
import { ProductionLines } from "@/pages/ProductionLines";

const ProtectedRoute = ({ component }: { component: ComponentType<object> }) => {
    const Component = withAuthenticationRequired(component);
    return <Component />;
};

export const AppRoutes = () => {
    return (
        <Routes>
            <Route element={<MainLayout />}>
                <Route index element={<Home />} />
                <Route path="profile" element={<ProtectedRoute component={Profile} />} />
                <Route path="resources" element={<ProtectedRoute component={Resources} />} />
                <Route path="resources/:resourceId" element={<ProtectedRoute component={ResourceDetails} />} />
                <Route path="nodes/:nodeId" element={<ProtectedRoute component={WorldNodeDetails} />} />
                <Route path="nodes/:nodeId/increase-extraction-rate" element={<ProtectedRoute component={IncreaseWorldNodeExtractionRate} />} />
                <Route path="nodes/:nodeId/decrease-extraction-rate" element={<ProtectedRoute component={DecreaseWorldNodeExtractionRate} />} />
                <Route path="production-lines" element={<ProtectedRoute component={ProductionLines} />} />
                <Route path="/loginError" element={<LoginError />} />
                <Route path="*" element={<NoMatch />} />
            </Route>
            <Route path="/loginRedirect" element={<LoginRedirect />} />
            <Route path="/login" element={<Login />} />
        </Routes>
    );
};
