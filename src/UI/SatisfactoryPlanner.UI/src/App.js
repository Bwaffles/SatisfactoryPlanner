import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

import Nav from "./Nav";

import Login from "./Login";
import Registration from "./Registration";
import ConfirmRegistration from "./ConfirmRegistration";
import Home from "./Home";
import Profile from "./Profile";

function App() {
    return (
        <Router>
            <div className="flex min-h-screen bg-gray-900 text-white">
                <Nav />
                <div className="m-4">
                    <Routes>
                        <Route path="/login" element={<Login />} />
                        <Route path="/registration" element={<Registration />} />
                        <Route path="/" element={<Home />} />
                        <Route path="/profile" element={<Profile />} />
                        <Route exact path="/confirm-registration/:registrationId" element={<ConfirmRegistration />} />
                    </Routes>
                </div>
            </div>

        </Router>
    );
}

export default App;
