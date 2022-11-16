import React, { useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import Auth from "./Auth/Auth";

interface CallbackProps {
    auth: Auth;
}

const Callback = ({ auth }: CallbackProps) => {
    const location = useLocation();
    const navigate = useNavigate();

    useEffect(() => {
        if (/access_token|id_token|error/.test(location.hash)) {
            auth.handleAuthentication(navigate);
        }
    });

    return (
        <h1>
            Loading...
        </h1>
    );
}

export default Callback;