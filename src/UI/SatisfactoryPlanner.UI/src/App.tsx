import * as React from "react";
import { Routes, Route } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

import AutoLogin from "./pages/AutoLogin";
import Home from "./pages/Home";
import Layout from "./pages/Layout";
import NoMatch from "./pages/NoMatch";
import Profile from "./pages/Profile";
import Resources from "./pages/Resources";
import Callback from "./pages/Callback";

const App = () => {
    const { isAuthenticated } = useAuth0();

    return (
        <Routes>
            <Route element={<Layout />}>
                <Route index element={<Home />} />
                <Route path="profile" element={isAuthenticated ? <Profile /> : <AutoLogin />} />
                <Route path="resources" element={isAuthenticated ? <Resources /> : <AutoLogin />} />
                <Route path="/callback" element={<Callback />} />
                <Route path="*" element={<NoMatch />} />
            </Route>
        </Routes>
    );
}

export default App;
