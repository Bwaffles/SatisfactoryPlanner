import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

import { Spinner } from "../components/Elements/Spinner";

// On login auth0 will redirect here and we will wait until the authentication process is finished.
// Once the user is authenticated we'll redirect to the Callback page to handle the rest of the process.
export const LoginRedirect = () => {
    const navigate = useNavigate();
    const { isAuthenticated } = useAuth0();

    useEffect(() => {
        if (isAuthenticated) {
            navigate("/login");
        }
    }, [isAuthenticated, navigate]);

    return (
        <div className="flex items-center justify-center w-screen h-screen bg-gray-900">
            <Spinner size="xl" />
        </div>
    );
};
