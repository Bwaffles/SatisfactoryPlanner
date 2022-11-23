import React from 'react';
import { useAuth0 } from "@auth0/auth0-react";

function AutoLogin() {
    const { loginWithRedirect, } = useAuth0();

    loginWithRedirect();

    return (
        <p>Logging in...</p>
    );
}

export default AutoLogin;