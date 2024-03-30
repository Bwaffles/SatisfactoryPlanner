import React from "react";
import { useAuth0 } from "@auth0/auth0-react";

export function LoginError() {
    const { isAuthenticated, logout } = useAuth0();

    if (isAuthenticated) {
        logout({ logoutParams: { returnTo: window.location.href }});
    }

    return <p>Something went wrong logging in.</p>;
}
