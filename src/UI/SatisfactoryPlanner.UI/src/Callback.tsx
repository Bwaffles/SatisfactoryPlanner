import React, { useEffect } from "react";
import { useLocation } from "react-router-dom";
import Auth from "./Auth/Auth";

interface CallbackProps {
    auth: Auth;
}

const Callback = ({ auth }: CallbackProps) => {
    const location = useLocation();

    useEffect(() => {
        if (/access_token|id_token|error/.test(location.hash)) {
            auth.handleAuthentication();
        } else {
            throw new Error("Invalid callback URL.");
        }
    });

    return (
        <h1>
            Loading...
        </h1>
    );
}

export default Callback;