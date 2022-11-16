import * as React from "react";
import { BrowserRouter as BrowserRouter, Routes, Route } from "react-router-dom";

import Nav from "./Nav";
import Auth from "./Auth/Auth"

import Login from "./Login";
import Registration from "./Registration";
import ConfirmRegistration from "./ConfirmRegistration";
import Home from "./Home";
import Profile from "./Profile";
import Callback from "./Callback";

function App() {
    const auth = new Auth();

    return (
        <BrowserRouter>
            <div className="flex min-h-screen bg-gray-900 text-white">
                <Nav auth={auth} />
                <div className="m-4">
                    <Routes>
                        <Route path="/" element={<Home auth={auth} />} />
                        <Route path="/profile" element={<Profile />} />
                        <Route path="/callback" element={<Callback auth={auth} />} />

                        <Route path="/login" element={<Login />} />
                        <Route path="/registration" element={<Registration />} />
                        <Route path="/confirm-registration/:registrationId" element={<ConfirmRegistration />} />
                    </Routes>
                </div>
            </div>
        </BrowserRouter>
    );
}

export default App;
