import * as React from "react";
import { BrowserRouter as Router, useNavigate } from "react-router-dom";
import { Auth0Provider } from "@auth0/auth0-react";

import { AppRoutes } from "./routes";

const Auth0ProviderWithRedirectCallback = ({ children, ...props }: any) => {
    const navigate = useNavigate();

    const onRedirectCallback = () => {
        // redirect to the callback page so that I can ensure the user is created after sign up
        navigate("/callback");
    };
    return (
        <Auth0Provider
            onRedirectCallback={onRedirectCallback}
            {...props}
        >
            {children}
        </Auth0Provider>
    );
};

const App = () => {
    return (
        <Router>
            <Auth0ProviderWithRedirectCallback
                domain={process.env.REACT_APP_AUTH0_DOMAIN!}
                clientId={process.env.REACT_APP_AUTH0_CLIENT_ID!}
                redirectUri="http://localhost:3000"
                audience="http://localhost:55915/api"
            >
                <AppRoutes />
            </Auth0ProviderWithRedirectCallback>
        </Router>
    );
};

export default App;
