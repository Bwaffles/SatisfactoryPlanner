import * as React from "react";
import { useNavigate } from "react-router-dom";
import { Routes, Route } from "react-router-dom";

import Auth from "./Auth/Auth"

import Nav from "./Nav";
import Home from "./Home";
import Profile from "./Profile";
import Callback from "./Callback";
import Login from "./Login";
import Registration from "./Registration";
import ConfirmRegistration from "./ConfirmRegistration";

const App = () => {
    // Passing navigate into auth and passing auth as props to the components means that the components will re-render when navigating to a new page
    const navigate = useNavigate();
    const auth = new Auth(navigate);

    return (
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
    );
}

export default App;
