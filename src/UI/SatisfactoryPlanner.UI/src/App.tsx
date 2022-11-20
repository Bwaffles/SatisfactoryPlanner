import * as React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

import Nav from "./Nav";
import Home from "./Home";
import Profile from "./Profile";
import Login from "./Login";
import Registration from "./Registration";
import ConfirmRegistration from "./ConfirmRegistration";

const App = () => {
    const { isAuthenticated } = useAuth0();

    return (
        <div className="flex min-h-screen bg-gray-900 text-white">
            <Nav />
            <div className="m-4 px-3 w-full h-full">
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route
                        path="/profile"
                        element={isAuthenticated ? <Profile  /> : <Navigate to={"/"} />}
                    />

                    <Route path="/login" element={<Login />} />
                    <Route path="/registration" element={<Registration />} />
                    <Route path="/confirm-registration/:registrationId" element={<ConfirmRegistration />} />
                </Routes>
            </div>
        </div>
    );
}

export default App;
