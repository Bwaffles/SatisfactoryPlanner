import * as React from "react";
import { BrowserRouter as Router, useNavigate } from "react-router-dom";
import { Auth0Provider } from "@auth0/auth0-react";

import * as Config from "./config";
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
                domain={Config.AUTH0_DOMAIN}
                clientId={Config.AUTH0_CLIENT_ID}
                redirectUri={Config.REDIRECT_URL}
                audience={Config.API_URL}
            >
                <AppRoutes />
            </Auth0ProviderWithRedirectCallback>
        </Router>
    );
};

export default App;
